using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Shapes.Base;

namespace Coursework.Shapes.Types
{
    public class Triangle : Shape
    {
        public int SideLength { get; private set; }
        private Point top;
        private Point bottomLeft;
        private Point bottomRight;
        private Point[] points;

        public Triangle(int sideLength, int x, int y, Color innerColor, Color borderColor) : base(sideLength, (int)(Math.Sqrt(3) / 2 * sideLength), x, y, innerColor, borderColor)
        {
            SideLength = sideLength;
        }

        public override int CalculateArea()
        {
            return (int)(Math.Sqrt(3) / 4 * Math.Pow(SideLength, 2));
        }

        public override int CalculatePerimeter()
        {
            return 3 * SideLength;
        }

        public override void OnPaint(object sender, PaintEventArgs e)
        {
            int upperLeftX = X - Width / 2;
            int upperLeftY = Y - Height / 2;
            int halfBase = SideLength / 2;

            top = new Point(X, Y - Height / 2);
            bottomLeft = new Point(X - halfBase, Y + Height / 2);
            bottomRight = new Point(X + halfBase, Y + Height / 2);

            points = new Point[] { top, bottomRight, bottomLeft };

            e.Graphics.DrawPolygon(new Pen(BorderColor, 4), points);
            e.Graphics.FillPolygon(new SolidBrush(InnerColor), points);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw selected border
            if (IsSelected)
            {
                using (Pen dottedPen = new Pen(Color.Black, 2))
                {
                    dottedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    int handleSize = 6;
 
                    e.Graphics.DrawRectangle(dottedPen, upperLeftX, upperLeftY, Width, Height);

                    Brush whiteBrush = Brushes.White;
 
                    e.Graphics.FillRectangle(whiteBrush, upperLeftX - handleSize / 2, upperLeftY - handleSize / 2, handleSize, handleSize);
                    e.Graphics.DrawRectangle(Pens.Black, upperLeftX - handleSize / 2, upperLeftY - handleSize / 2, handleSize, handleSize);

                    e.Graphics.FillRectangle(whiteBrush, upperLeftX + Width - handleSize / 2, upperLeftY - handleSize / 2, handleSize, handleSize);
                    e.Graphics.DrawRectangle(Pens.Black, upperLeftX + Width - handleSize / 2, upperLeftY - handleSize / 2, handleSize, handleSize);

                    e.Graphics.FillRectangle(whiteBrush, upperLeftX - handleSize / 2, upperLeftY + Height - handleSize / 2, handleSize, handleSize);
                    e.Graphics.DrawRectangle(Pens.Black, upperLeftX - handleSize / 2, upperLeftY + Height - handleSize / 2, handleSize, handleSize);
 
                    e.Graphics.FillRectangle(whiteBrush, upperLeftX + Width - handleSize / 2, upperLeftY + Height - handleSize / 2, handleSize, handleSize);
                    e.Graphics.DrawRectangle(Pens.Black, upperLeftX + Width - handleSize / 2, upperLeftY + Height - handleSize / 2, handleSize, handleSize);
                }
            }
        }

        public override void UpdateLocation(int XOnMouseDown, int YOnMouseDown, int XOnMouseMove, int YOnMouseMove,
            int canvasXLeft, int canvasXRight, int canvasYTop, int canvasYBottom)
        {
            int deltaX = XOnMouseMove - XOnMouseDown;
            int deltaY = YOnMouseMove - YOnMouseDown;

            if (X + deltaX - Width / 2 < canvasXLeft || X + deltaX + Width / 2 > canvasXRight ||
                Y + deltaY - Height / 2 < canvasYTop || Y + deltaY + Height / 2 > canvasYBottom)
                return;

            X += deltaX;
            Y += deltaY;
        }

        public override void UpdatePropreties(object[] parameters)
        {
            BorderColor = (Color)parameters[0];
            InnerColor = (Color)parameters[1];
            SideLength = ((List<int>)parameters[2])[0];
            Width = SideLength;
            Height = (int)(Math.Sqrt(3) / 2 * SideLength);
        }

        public override bool IsMouseInside(int mouseX, int mouseY)
        {
            int d1 = (bottomLeft.X - top.X) * (mouseY - top.Y) - (bottomLeft.Y - top.Y) * (mouseX - top.X);
            int d2 = (bottomRight.X - bottomLeft.X) * (mouseY - bottomLeft.Y) - (bottomRight.Y - bottomLeft.Y) * (mouseX - bottomLeft.X);
            int d3 = (top.X - bottomRight.X) * (mouseY - bottomRight.Y) - (top.Y - bottomRight.Y) * (mouseX - bottomRight.X);
            return d1 > 0 && d2 > 0 && d3 > 0 || d1 < 0 && d2 < 0 && d3 < 0;
        }
    }
}
