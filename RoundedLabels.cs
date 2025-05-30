using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D; // this helps bring in specific styling for the class
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// this is a custom class that makes the labels have a rounded appearance.
// it's almost used like a function call
// so instead of label1.Label you'd do Label1.RoundedLabel to call this class.
namespace PDFExtractor
{
    public class RoundedLabel : Label
    {
        private int borderRadius = 15; // defining the curvature of the labels, the bigger the number the sharper the curve
        private Color insideBackColor = Color.MediumSeaGreen; // color for the inside of the rounded rectangle (specific to this program)
       
        // invalidate() forces a repaint when the values change
        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; Invalidate(); }
        }

        public Color InsideBackColor
        {
            get { return insideBackColor; }
            set { insideBackColor = value; Invalidate(); }
        }

        public RoundedLabel() // enables custom drawing by setting UserPaint style
        { 
            SetStyle(ControlStyles.UserPaint, true); 
        }

        // override the OnPaint method to customize label rendering
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // improve rendering quality for smoother curves

            // fill the entire background with white
            using (Brush whiteBrush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(whiteBrush, ClientRectangle);
            }

            // draw the rounded rectangle with the inside background color
            using (GraphicsPath path = GetRoundedRectanglePath(ClientRectangle, borderRadius))
            {
                using (Brush brush = new SolidBrush(insideBackColor))
                {
                    e.Graphics.FillPath(brush, path); // fill the shape with the inside background color
                }
                using (Pen pen = new Pen(ForeColor))
                {
                    e.Graphics.DrawPath(pen, path); // outline the rounded shape with the label's foreground color
                }
            }

            // draw the text within its bounds
            Rectangle textRect = ClientRectangle;
            textRect.Inflate(0, 0); // Adjust to avoid text clipping
            TextRenderer.DrawText(e.Graphics, Text, Font, textRect, ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        // helper method to create a path for smoother edges
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            int diameter = radius * 2; // calculate the full arc diameter
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            // define the rounded corners using arcs
            path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e); // ensure the shape is fully connected
            Invalidate(); // redraw the control when resized
        }
    }
}
