// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// Animat.UI/JsonUtils.cs
// --------------------------------------------------------------------------------
// Copyright (c) 2014, Jieni Luchijinzhou a.k.a Aragorn Wyvernzora
// 
// This file is a part of Animat.UI.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal 
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
// of the Software, and to permit persons to whom the Software is furnished to do 
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using libWyvernzora.IO;

namespace Animat.Foundation
{
    /// <summary>
    ///     A bunch of utilities to (de)serialize stuff.
    /// </summary>
    public static class JsonUtils
    {
        /// <summary>
        ///     Serializes an object to a JSON stream.
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize.</typeparam>
        /// <param name="a">The object to serialize.</param>
        /// <param name="stream">Destination stream.</param>
        public static void Serialize<T>(this T a, Stream stream)
        {
            // Check Arguments
            if (a == null) throw new ArgumentNullException("a");
            if (stream == null) throw new ArgumentNullException("stream");

            // Serialize
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, a);
        }

        /// <summary>
        ///     Deserializes an object from a JSON stream.
        /// </summary>
        /// <typeparam name="T">Type of the object to deserialize.</typeparam>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        public static T Deserialize<T>(StreamEx stream)
        {
            // Check arguments
            if (stream == null) throw new ArgumentNullException("stream");

            // Deserialize
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            return (T) serializer.ReadObject(stream);
        }
    }
}