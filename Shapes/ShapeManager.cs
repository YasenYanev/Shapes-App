using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Interfaces;

namespace Coursework.Shapes
{
    public class ShapeManager
    {
        public List<Shape> shapesList = new List<Shape> { };
        public Shape? selectedShape = null;
        private Form1 _mainForm;
        public ShapeManager(Form1 form) {
            _mainForm = form;
        }

        public void DeleteShape(Shape shape)
        {
            _mainForm.panelCanvas.Paint -= shape.OnPaint;
            shapesList.Remove(shape);
        }

        public void AddShape(string shapeType, Color innerColor, Color borderColor, List<int> props)
        {
            Shape shape = null;
            switch (shapeType)
            {
                case "Triangle":
                    shape = new Triangle(props.First(), 125, 125, innerColor, borderColor);
                    break;
                case "Circle":
                    shape = new Circle(props.First(), 125, 125, innerColor, borderColor);
                    break;
                case "Square":
                    shape = new RectangleS(props.First(), props.First(), 125, 125, innerColor, borderColor);
                    break;
                case "Rectangle":
                    shape = new RectangleS(props.First(), props.Last(), 125, 125, innerColor, borderColor);
                    break;
            }
            if (shape != null)
            {
                _mainForm.panelCanvas.Paint += shape.OnPaint;
                shapesList.Add(shape);

                _mainForm.shapeInteractionHandler.OnTryShapeSelect(shape);
                _mainForm.panelCanvas.Refresh();
            }
        }
        public void EditShape(Color selectedInnerColor, Color selectedBorderColor, List<int> props)
        {
            if (selectedShape is Triangle) selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, props.First());
            else if (selectedShape is Circle) selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, props.First());
            else if (props.Count == 1) selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, props.First(), props.First());
            else selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, props.First(), props.Last());
        }
    }
}