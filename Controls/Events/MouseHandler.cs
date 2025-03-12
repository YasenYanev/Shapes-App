using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coursework;
using Coursework.Interfaces;
using Coursework.Shapes;

namespace Coursework.Controls.Events
{
    public class MouseHandler : IMouseHandler
    {
        Form1 mainScene;
        private Point lastMousePosition = new Point(0, 0);
        public delegate void TryShapeSelectHandler(Shape shape);
        public event TryShapeSelectHandler TryShapeSelect;

        public MouseHandler(Form1 form)
        {
            mainScene = form;
            TryShapeSelect += mainScene.OnTryShapeSelect;
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            HandleShapeSelection(e);
            mainScene.panelCanvas.Refresh();
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            HandleShapeSelection(e);
            mainScene.panelCanvas.Refresh();
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            foreach (Shape shape in mainScene.shapesList)
            {
                if (e.Button == MouseButtons.Left && shape.IsSelected)
                {
                    shape.UpdateLocation(lastMousePosition.X, lastMousePosition.Y, e.X, e.Y,
                        0, mainScene.panelCanvas.Width, 0, mainScene.panelCanvas.Height);
                    mainScene.Cursor = Cursors.SizeAll;
                    mainScene.shapePropertiesForm.UpdateShapeProperties(e.X, e.Y);
                    break;
                }
                else if (shape.IsMouseInside(e.X, e.Y)) mainScene.Cursor = Cursors.Hand;
                else mainScene.Cursor = Cursors.Default;
            }

            UpdateLastMousePosition(e);
            mainScene.panelCanvas.Refresh();
        }

        private void UpdateLastMousePosition(MouseEventArgs e)
        {
            lastMousePosition.X = e.X;
            lastMousePosition.Y = e.Y;
        }

        private void HandleShapeSelection(MouseEventArgs e)
        {
            Shape? shapeToSelect = null;
            foreach (Shape shape in mainScene.shapesList)
            {
                if (shape.IsMouseInside(e.X, e.Y))
                {
                    shapeToSelect = shape;
                    break;
                }
            }
            TryShapeSelect?.Invoke(shapeToSelect);
        }
    }
}
