using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coursework.Interfaces;
using Coursework.Shapes;

namespace Coursework.Interaction.Handlers
{
    public class ShapeInteractionHandler : IShapeInteractionHandler
    {
        private Form1 _mainForm;
        private Point lastMousePosition = new Point(0, 0);
        public delegate void TryShapeSelectHandler(Shape shape);
        public event TryShapeSelectHandler TryShapeSelect;

        public ShapeInteractionHandler(Form1 form)
        {
            _mainForm = form;
            TryShapeSelect += OnTryShapeSelect;
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            HandleShapeSelection(e);
            _mainForm.panelCanvas.Refresh();
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            HandleShapeSelection(e);
            _mainForm.panelCanvas.Refresh();
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            foreach (Shape shape in _mainForm.shapeManager.shapesList)
            {
                if (e.Button == MouseButtons.Left && shape.IsSelected)
                {
                    shape.UpdateLocation(lastMousePosition.X, lastMousePosition.Y, e.X, e.Y,
                        0, _mainForm.panelCanvas.Width, 0, _mainForm.panelCanvas.Height);
                    _mainForm.Cursor = Cursors.SizeAll;
                //    _mainForm.shapePropertiesForm.UpdateShapeProperties(shape.X, shape.Y); // Fix: update x and y based on shape center not mouse position
                    break;
                }
                else if (shape.IsMouseInside(e.X, e.Y)) _mainForm.Cursor = Cursors.Hand;
                else _mainForm.Cursor = Cursors.Default;
            }

            UpdateLastMousePosition(e);
            _mainForm.panelCanvas.Refresh();
        }

        private void UpdateLastMousePosition(MouseEventArgs e)
        {
            lastMousePosition.X = e.X;
            lastMousePosition.Y = e.Y;
        }
        public void OnTryShapeSelect(Shape? shape)
        {
            if (shape != null && shape == _mainForm.shapeManager.selectedShape) return;
            if (_mainForm.shapeManager.selectedShape != null) _mainForm.shapeManager.selectedShape.IsSelected = false;
            if (shape == null)
            {
                _mainForm.shapeManager.selectedShape = null;
                _mainForm.panelProperties.Controls.Clear();
                _mainForm.panelCanvas.Refresh();
                return;
            }

            _mainForm.shapeManager.selectedShape = shape;
            _mainForm.shapeManager.selectedShape.IsSelected = true;


            _mainForm.createPropretiesForm();
            _mainForm.panelCanvas.Refresh();
        }
        private void HandleShapeSelection(MouseEventArgs e)
        {
            Shape? shapeToSelect = null;
            foreach (Shape shape in _mainForm.shapeManager.shapesList)
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
