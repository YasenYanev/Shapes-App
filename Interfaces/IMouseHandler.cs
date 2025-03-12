namespace Coursework.Interfaces
{
    public interface IMouseHandler
    {
        void OnMouseClick(object sender, MouseEventArgs e);
        void OnMouseDown(object sender, MouseEventArgs e);
        void OnMouseMove(object sender, MouseEventArgs e);
    }
}