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
        private readonly ShapeFactory _shapeFactory = new ShapeFactory();
        private Form1 _mainForm;
        public ShapeManager(Form1 form) {
            _mainForm = form;
            _shapeFactory.AddShape("Triangle", (List<int> props, Color innerColor, Color borderColor) 
                => new Triangle(props.First(), 125, 125, innerColor, borderColor));
            _shapeFactory.AddShape("Circle", (List<int> props, Color innerColor, Color borderColor)
                => new Circle(props.First(), 125, 125, innerColor, borderColor));
            _shapeFactory.AddShape("Square", (List<int> props, Color innerColor, Color borderColor)
                => new RectangleS(props.First(), props.First(), 125, 125, innerColor, borderColor));
            _shapeFactory.AddShape("Rectangle", (List<int> props, Color innerColor, Color borderColor)
                => new RectangleS(props.First(), props.Last(), 125, 125, innerColor, borderColor));
        }

        public void DeleteShape(Shape shape)
        {
            _mainForm.panelCanvas.Paint -= shape.OnPaint;
            shapesList.Remove(shape);
        }

        public void AddShape(string shapeType, List<int> props, Color innerColor, Color borderColor)
        {
            Shape shape = _shapeFactory.CreateShape(shapeType, props, innerColor, borderColor);

            _mainForm.panelCanvas.Paint += shape.OnPaint;
            shapesList.Add(shape);

            _mainForm.shapeInteractionHandler.OnTryShapeSelect(shape);
            _mainForm.panelCanvas.Refresh();
        }
        public void EditShape(Color selectedInnerColor, Color selectedBorderColor, List<int> props)
        {
            selectedShape.UpdatePropreties(new object[] { selectedBorderColor, selectedInnerColor, props });
        }
    }
}