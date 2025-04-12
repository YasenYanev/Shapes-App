namespace Coursework.Interfaces
{
    public interface IShapeInteractionHandler
    {
        void OnMouseClick(object sender, MouseEventArgs e);
        void OnMouseDown(object sender, MouseEventArgs e);
        void OnMouseMove(object sender, MouseEventArgs e);
    }
}