using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Shapes.Base;

namespace Coursework.Shapes.Types
{
    public class RectangleS : Shape
    {
        public RectangleS(int width, int height, int x, int y, Color innerColor, Color borderColor) : base(width, height, x, y, innerColor, borderColor)
        {
        }

        public override int CalculateArea()
        {
            return Width * Height;
        }

        public override int CalculatePerimeter()
        {
            return 2 * (Width + Height);
        }

        public override void OnPaint(object sender, PaintEventArgs e)
        {
            int upperLeftX = X - Width / 2;
            int upperLeftY = Y - Height / 2;

            e.Graphics.DrawRectangle(new Pen(BorderColor, 4), upperLeftX, upperLeftY, Width, Height);
            e.Graphics.FillRectangle(new SolidBrush(InnerColor), upperLeftX, upperLeftY, Width, Height);
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
            if(((List<int>)args[2]).Count == 2)
            {
                Width = ((List<int>)args[2])[0];
                Height = ((List<int>)args[2])[1];
            }else
            {
                Width = ((List<int>)args[2])[0];
                Height = Width;
            }
        }
        public override bool IsMouseInside(int mouseX, int mouseY)
        {
            return mouseX >= X - Width / 2 && mouseX <= X + Width / 2 && mouseY >= Y - Height / 2 && mouseY <= Y + Height / 2;
        }
    }
}
