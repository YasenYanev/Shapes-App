using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Shapes.Base
{
    class ShapeFactory
    {
        private readonly Dictionary<string, Func<List<int>, Color, Color, Shape>> _shapesFactory 
            = new Dictionary<string, Func<List<int>, Color, Color, Shape>>();
        public void RegisterShape(string shapeType, Func<List<int>, Color, Color, Shape> shapeFunc)
        {
            _shapesFactory[shapeType] = shapeFunc;
        }
        public Shape CreateShape(string shapeType, List<int> props, Color innerColor, Color borderColor)
        {
            if (_shapesFactory.ContainsKey(shapeType)) return _shapesFactory[shapeType](props, innerColor, borderColor);
            else throw new Exception($"Type of {shapeType} does not exist");
        }

    }
}
