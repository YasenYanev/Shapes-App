namespace Coursework.Interfaces
{
    public interface IShape
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public Color InnerColor { get; }
        public Color BorderColor { get; }
        public bool IsSelected { get; }

        int CalculateArea();
        int CalculatePerimeter();
        void OnPaint(object sender, PaintEventArgs e);
        void UpdateLocation(int XOnLastEvent, int YOnLastEvent, int XOnMouseMove, int YOnMouseMove,
            int canvasXLeft, int canvasXRight, int canvasYTop, int canvasYBottom);
        void UpdatePropreties(params object[] parameters);
        bool IsMouseInside(int X, int Y);
    }

}