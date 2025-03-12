using Coursework.Shapes;
using Coursework.Controls.Events;
using System.Windows.Forms;
using Coursework.Forms;

namespace Coursework
{
    public partial class Form1 : Form
    {
        private MouseHandler mouseHandler;
        public List<Shape> shapesList = new List<Shape> { };
        public Shape? selectedShape = null;
        public ShapePropertiesForm shapePropertiesForm;
        public Form1()
        {
            InitializeComponent();
            mouseHandler = new MouseHandler(this);
            panelCanvas.MouseClick += mouseHandler.OnMouseClick;
            panelCanvas.MouseDown += mouseHandler.OnMouseDown;
            panelCanvas.MouseMove += mouseHandler.OnMouseMove;
        }


    }
}
