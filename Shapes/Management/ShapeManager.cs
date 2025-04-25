using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Shapes.Base;
using Coursework.Shapes.Types;
namespace Coursework.Shapes.Management
{
    public class ShapeManager
    {
        public List<Shape> shapesList = new List<Shape> { };
        public Shape? selectedShape = null;
        private readonly ShapeFactory _shapeFactory = new ShapeFactory();
        private MainForm _mainForm;
        public ShapeManager(MainForm form) {
            _mainForm = form;
            _shapeFactory.RegisterShape("Triangle", (props, innerColor, borderColor) 
                => new Triangle(props.First(), 125, 125, innerColor, borderColor));
            _shapeFactory.RegisterShape("Circle", (props, innerColor, borderColor)
                => new Circle(props.First(), 125, 125, innerColor, borderColor));
            _shapeFactory.RegisterShape("Square", (props, innerColor, borderColor)
                => new RectangleS(props.First(), props.First(), 125, 125, innerColor, borderColor));
            _shapeFactory.RegisterShape("Rectangle", (props, innerColor, borderColor)
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
        public void EditShape(List<int> props, Color selectedInnerColor, Color selectedBorderColor)
        {
            selectedShape.UpdatePropreties(new object[] { selectedBorderColor, selectedInnerColor, props });
        }
    }
}