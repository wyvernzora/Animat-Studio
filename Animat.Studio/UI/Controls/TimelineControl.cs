using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Animat.Studio.Properties;

namespace Animat.Studio.UI.Controls
{
    public partial class TimelineControl : Control
    {
        #region Constants

        private const Int32 TimeScaleHeight = 30;

        private readonly Font TickLabelFont = new Font("Arial", 9.0F);
        private readonly Color SeparatorColor = Color.DarkGray;
        private readonly Color AccentLayerBackground = Color.FromArgb(221, 214, 255);

        private readonly Cursor grab;

        #endregion

        public TimelineControl()
        {
            InitializeComponent();

            // Load the cursor
            using (var stream = new MemoryStream(Resources.closedhand))
            { grab = new Cursor(stream); }

            // Setup the form so that it is custom-painted and double buffered
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            // Separator Styles
            SeparatorLocation = 250;


            // Tick Styles
            ScaleStart = 3;
            TickFrequency = 10;
            TickAccentFrequency = 5;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the start offset of the timeline scale.
        /// </summary>
        public Int32 ScaleStart { get; set; }
        /// <summary>
        /// Gets or sets the frequency of ticks (in pixels).
        /// </summary>
        public Int32 TickFrequency { get; set; }
        /// <summary>
        /// Gets of sets the frequency of accented ticks (in number of ticks).
        /// </summary>
        public Int32 TickAccentFrequency { get; set; }
        /// <summary>
        /// Gets or sets the location of the separator line
        /// </summary>
        public Int32 SeparatorLocation { get; set; }
        /// <summary>
        /// Gets or sets the height or each layer.
        /// </summary>
        public Int32 LayerHeight { get; set; }

        /// <summary>
        /// Gets or sets the position of the caret.
        /// </summary>
        public Int32 CaretPosition { get; set; }

        #endregion
        
        protected override void OnPaint(PaintEventArgs e)
        {
#if DEBUG
            var stopwatch = new Stopwatch();
            stopwatch.Start();
#endif

            // Draw Layers and such
            for (int i = 0; i < 3; i++)
            {
                // Determine whether to use accent color
                var color = i % 2 == 1 ? BackColor : AccentLayerBackground;


                // Draw layer box
                using (var accentBrush = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(accentBrush, 0, TimeScaleHeight + i * LayerHeight + 1, Width, LayerHeight);
                }

                // Draw Layer Name
                using (var foreBrush = new SolidBrush(ForeColor))
                {
                    e.Graphics.DrawString("Layer " + i, Font, foreBrush, 
                        new RectangleF(0, TimeScaleHeight + i * LayerHeight, SeparatorLocation, LayerHeight) ,
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }

                // Draw separator line
                using (var separatorBrush = new SolidBrush(SeparatorColor))
                {
                    var y = TimeScaleHeight + (i + 1) * LayerHeight;
                    e.Graphics.DrawLine(new Pen(separatorBrush, 1), 0, y, Width, y);
                }
            }

            // Draw Scale Ticks
            using (var tickBrush = new SolidBrush(ForeColor))
            {
                // Calculate the number of ticks on the scale
                var numTicks = (Width - SeparatorLocation) / TickFrequency + 1;

                // Draw the scale
                for (int i = ScaleStart; i < (ScaleStart + numTicks); i++)
                {
                    // Calculate the tick length
                    var scaleHeight = i % TickAccentFrequency == 0 ? 10 : 5;
                    // Calculate the actual coordinate where to draw the tick
                    var drawX = (i - ScaleStart) * TickFrequency + SeparatorLocation;
                    // Actually draw it
                    e.Graphics.DrawLine(new Pen(tickBrush, 1), drawX, 0, drawX, scaleHeight);
                    
                    // Draw tick label if this is an accent tick
                    if (i % TickAccentFrequency == 0)
                    {
                        var labelSize = e.Graphics.MeasureString(i.ToString(), TickLabelFont);
                        drawX -= (Int32)labelSize.Width / 2;

                        if (drawX >= SeparatorLocation)
                            e.Graphics.DrawString(i.ToString(), TickLabelFont, tickBrush, drawX, scaleHeight);
                    }
                }
            }



            // Draw Separator Lines
            using (var lineBrush = new SolidBrush(SeparatorColor))
            {
                // Draw Separator Lines
                e.Graphics.DrawLine(new Pen(lineBrush, 1), SeparatorLocation, 0, SeparatorLocation, Height);
                e.Graphics.DrawLine(new Pen(lineBrush, 1), 0, TimeScaleHeight, Width, TimeScaleHeight);
            }

            // Process other paintjobs
            base.OnPaint(e);

#if DEBUG
            stopwatch.Stop();
            e.Graphics.DrawString(stopwatch.ElapsedMilliseconds + "ms", Font, new SolidBrush(Color.DarkRed), 0, 0);
#endif

        }

        #region Mouse Pan Handling

        private Int32 originalScaleStart;
        private Point mouseMoveStart;
        private Boolean isDragging;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Handle the base stuff
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (!isDragging && e.Location.X >= SeparatorLocation)
                {
                    originalScaleStart = ScaleStart;
                    mouseMoveStart = e.Location;
                    isDragging = true;
                    Cursor = grab;
                }
                if (isDragging) 
                {
                    int dx = mouseMoveStart.X - e.Location.X;
                    int dy = mouseMoveStart.Y - e.Location.Y;

                    UpdateScrollPosition(dx, dy);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (isDragging) {
                isDragging = false;
                Cursor = Cursors.Default;
            }
        }
        
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!isDragging && e.Location.X >= SeparatorLocation)
            {
                UpdateScrollPosition(e.Delta * TickFrequency, 0);
            }
        }

        private void UpdateScrollPosition(Int32 dx, Int32 dy)
        {
            // Handle DX
            var scaleStartChange = dx / TickFrequency;
            ScaleStart = originalScaleStart + scaleStartChange;
            if (ScaleStart < 0)
                ScaleStart = 0;

            // Redraw
            Invalidate();
        }

        #endregion
    }
}
