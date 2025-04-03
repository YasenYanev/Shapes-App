using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Coursework.Shapes
{
    public class Circle : Shape
    {

        public int Radius { get; private set; }

        public Circle(int diameter, int x, int y, Color innerColor, Color borderColor) : base(diameter, diameter, x, y, innerColor, borderColor)
        {
            Radius = diameter / 2;
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
                    return; // Don't move if out of bounds

                X += deltaX;
                Y += deltaY;
        }

        public override void UpdatePropreties(params object[] parameters) {
            BorderColor = (Color)parameters[0];
            InnerColor = (Color)parameters[1];
            Radius = (int)parameters[2];
        }

        public override bool IsMouseInside(int mouseX, int mouseY)
        {
            return ((mouseX - X) * (mouseX - X) + (mouseY - Y) * (mouseY - Y) <= Radius * Radius);
        }
    }
}
