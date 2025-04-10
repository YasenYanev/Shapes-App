using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Interfaces;
using Newtonsoft.Json;

namespace Coursework.Shapes
{
    public abstract class Shape : IShape
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Color InnerColor { get; protected set; }
        public Color BorderColor { get; protected set; }
        // Real value for logic in your program
        [JsonIgnore]
        public bool IsSelected { get; set; } = false;
        // This always returns false when serializing
        [JsonProperty("IsSelected")]
        public bool IsSelectedSerialized => false;

        public Shape(int width, int height, int x, int y, Color innerColor, Color borderColor)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
            InnerColor = innerColor;
            BorderColor = borderColor;
        }

        public abstract int CalculateArea();
        public abstract int CalculatePerimeter();
        public abstract void OnPaint(object sender, PaintEventArgs e);
        public abstract void UpdateLocation(int XOnLastEvent, int YOnLastEvent, int XOnMouseMove, int YOnMouseMove,
            int canvasXLeft, int canvasXRight, int canvasYTop, int canvasYBottom);
        public abstract void UpdatePropreties(params object[] parameters);
        public abstract bool IsMouseInside(int X, int Y);
    }
}
