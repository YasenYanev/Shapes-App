using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Interfaces;

namespace Coursework.Shapes
{
    public class Shape : IShape
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Color InnerColor { get; protected set; }
        public Color BorderColor { get; protected set; }
        public bool IsSelected { get; set; } = false;

        public Shape(int width, int height, int x, int y, Color innerColor, Color borderColor)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
            InnerColor = innerColor;
            BorderColor = borderColor;
        }

        public virtual int CalculateArea()
        {
            throw new NotImplementedException();
        }
        public virtual int CalculatePerimeter()
        {
            throw new NotImplementedException();
        }
        public virtual void OnPaint(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }
        public virtual void UpdateLocation(int XOnLastEvent, int YOnLastEvent, int XOnMouseMove, int YOnMouseMove,
            int canvasXLeft, int canvasXRight, int canvasYTop, int canvasYBottom)
        {
            throw new NotImplementedException();
        }
        public virtual bool IsMouseInside(int X, int Y)
        {
            throw new NotImplementedException();
        }
    }
}
