using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Shapes.Base;

namespace Coursework.Shapes.Types
{
    public class Circle : Shape
    {

        public int Radius { get; private set; }

        public Circle(int radius, int x, int y, Color innerColor, Color borderColor) : base(radius * 2, radius * 2, x, y, innerColor, borderColor)
        {
            Radius = radius;
        }

        public override int CalculateArea()
        {
            return (int)(Math.Pow(Radius, 2) * Math.PI);
        }

        public override int CalculatePerimeter()
        {
            return (int)(2 * Math.PI * Radius);
        }

        public override void OnPaint(object sender, PaintEventArgs e)
        {
            int upperLeftX = X - Width / 2;
            int upperLeftY = Y - Height / 2;

            e.Graphics.DrawEllipse(new Pen(BorderColor, 4), upperLeftX, upperLeftY, Width, Height);
            e.Graphics.FillEllipse(new SolidBrush(InnerColor), upperLeftX, upperLeftY, Width, Height);
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

        public override void UpdateLocation(int XOnLastEvent, int YOnLastEvent, int XOnMouseMove, int YOnMouseMove,
            int canvasXLeft, int canvasXRight, int canvasYTop, int canvasYBottom)
        {
            int deltaX = XOnMouseMove - XOnLastEvent;
            int deltaY = YOnMouseMove - YOnLastEvent;

            if (X + deltaX - Width / 2 < canvasXLeft || X + deltaX + Width / 2 > canvasXRight ||
                Y + deltaY - Height / 2 < canvasYTop || Y + deltaY + Height / 2 > canvasYBottom)
                return;

            X += deltaX;
            Y += deltaY;
        }

        public override void UpdatePropreties(params object[] args)
        {
            BorderColor = (Color)args[0];
            InnerColor = (Color)args[1];
            Radius = ((List<int>)args[2])[0];
            Width = Radius * 2;
            Height = Radius * 2;
        }

        public override bool IsMouseInside(int mouseX, int mouseY)
        {
            return (mouseX - X) * (mouseX - X) + (mouseY - Y) * (mouseY - Y) <= Radius * Radius;

        }
    }
}
