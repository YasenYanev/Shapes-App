using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Shapes
{
    public class Square : RectangleS
    {
        public Square(int sideLength, int x, int y, Color innerColor, Color borderColor) : base(sideLength, sideLength, x, y, innerColor, borderColor)
        {
        }
    }
}
