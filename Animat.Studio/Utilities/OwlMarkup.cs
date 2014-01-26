using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using libWyvernzora.Core;
using libWyvernzora.IO;

namespace libWyvernzora.Owl.Markup
{
    /* OwlNode Structure
     * 
     * VInt     NodeID
     * VInt     ChildCount
     * VInt     DataLength      (Excludes NodeID and NodeLength)
     * 
     * OwlNode*    Children
     * 
     * Byte[]   Data
     * 
     */

    /// <summary>
    /// Owl Binary Markup OwlNode
    /// </summary>
    public class OwlNode
    {

        #region Private Fields

        // OwlNode ID
        protected VInt id;

        // Children
        protected List<OwlNode> children;

        // OwlNode stream, can be empty
        protected StreamEx data;

        #endregion


        /// <summary>
        /// Default constructor.
        /// Initializes the children collection.
        /// </summary>
        protected OwlNode()
        {
            id = new VInt(-1);
            children = new List<OwlNode>();
        }

        /// <summary>
        /// Constructor.
        /// Create an empty node with the specified ID.
        /// Length is calculated at write-time.
        /// </summary>
        /// <param name="id">Desired ID for the new node.</param>
        public OwlNode(Int64 id)
            : this()
        {
            this.id = new VInt(id);
            data = new StreamEx(new MemoryStream()); // Buffer for stream.
        }

        /// <summary>
        /// Constructor.
        /// Read a node from extended stream.
        /// </summary>
        /// <param name="stream">StreamEx object to read from.</param>
        public OwlNode(StreamEx stream)
            : this()
        {
#if !DEBUG
            try
            {
#endif
            // Read OwlNode ID
            id = stream.ReadVInt();

            // Read Child Count
            var childCount = stream.ReadVInt();

            // Read Data Length
            var dataLength = stream.ReadVInt();

            // Read Child Nodes
            for (int i = 0; i < childCount; i++)
                children.Add(new OwlNode(stream));

            // Read Data
            Int64 dataStart = stream.Position;
            data = new PartialStreamEx(stream, dataStart, dataLength);

            // Debug Asserts
            Debug.Assert((dataStart + dataLength) < stream.Length, "Invalid Data Length or Position",
                "dataStart = {0}; dataLength = {1}", dataStart, dataLength);
#if !DEBUG
            }
            catch (Exception x)
            {
                throw new InvalidDataException("Failed to read Owl.OwlNode from stream.", x);
            }
#endif
        }


        #region Properties

        /// <summary>
        /// Gets the ID of the node.
        /// </summary>
        public Int64 ID
        {
            get { return id; }
        }

        /// <summary>
        /// Gets the list of child nodes.
        /// </summary>
        public List<OwlNode> Children
        { get { return children; } }

        /// <summary>
        /// Gets the stream associated with the node.
        /// </summary>
        public StreamEx Data
        {
            get { return data; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set data associated with the node.
        /// </summary>
        /// <param name="stream"></param>
        public void SetData(Stream stream)
        {
            // TODO Dispose old stream stream (how?)

            // Assign new stream
            data = (stream as StreamEx) ?? new StreamEx(stream);
        }

        /// <summary>
        /// Writes the node to stream.
        /// </summary>
        /// <param name="stream">StreamEx object to write to.</param>
        public void WriteToStream(StreamEx stream)
        {
            // Write ID
            stream.WriteVInt(id);

            // Write Child Count
            stream.WriteVInt(new VInt(children.Count));

            // Write Data Length (0 if null)
            stream.WriteVInt(new VInt(data == null ? 0 : data.Length));

            // Write Children
            foreach (var child in children)
                child.WriteToStream(stream);

            // Write Data
            data.Position = 0;
            if (data != null)
                data.CopyTo(stream);
        }

        #endregion

        #region Navigation Methods

        /// <summary>
        /// Returns the first child node of the specified ID.
        /// If no matching node is found, returns null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OwlNode FindChild(Int64 id)
        {
            return children.Find(node => node.ID == id);
        }

        /// <summary>
        /// Returns all nodes with the specified ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OwlNode[] FindChildren(Int64 id)
        {
            return (from node in children where node.ID == id select node).ToArray();
        }

        #endregion
    }
}