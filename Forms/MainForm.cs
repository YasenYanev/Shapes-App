using System;
using System.Windows.Forms;
using Coursework.Forms;
using Coursework.Interaction;
using Coursework.Operations.Base;
using Coursework.Operations.Types;
using Coursework.Shapes.Base;
using Coursework.Shapes.Types;
using Coursework.Utilities;

namespace Coursework
{
    public partial class MainForm : Form
    {
        public ShapeFactory shapeFactory = new ShapeFactory();
        public OperationFactory operationFactory = new OperationFactory();
        public FormFactory formFactory = new FormFactory();

        public AddShapeForm? addShapeForm;
        public ShapePropertiesForm? shapePropertiesForm;
        public EditShapePropertiesForm? editShapeForm;
        public ShapesInfoForm? shapesInfoForm;

        public Shape? selectedShape = null;
        public List<Shape> shapesList = new List<Shape>();

        public MainForm()
        {
            InitializeComponent();

            shapeFactory.RegisterShape("Triangle", (props, innerColor, borderColor)
                => new Triangle(props.First(), 125, 125, innerColor, borderColor));
            shapeFactory.RegisterShape("Circle", (props, innerColor, borderColor)
                => new Circle(props.First(), 125, 125, innerColor, borderColor));
            shapeFactory.RegisterShape("Square", (props, innerColor, borderColor)
                => new RectangleS(props.First(), props.First(), 125, 125, innerColor, borderColor));
            shapeFactory.RegisterShape("Rectangle", (props, innerColor, borderColor)
                => new RectangleS(props.First(), props.Last(), 125, 125, innerColor, borderColor));

            operationFactory.RegisterOperation(new AddOperation(this));
            operationFactory.RegisterOperation(new EditOperation(this));
            operationFactory.RegisterOperation(new DeleteOperation(this));
            operationFactory.RegisterOperation(new SelectOperation(this));

            formFactory.RegisterForm("Add Form", () =>
            {
                panelProperties.Controls.Clear();
                AddShapeForm addShapeForm = new AddShapeForm(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    BackColor = panelProperties.BackColor
                };
                panelProperties.Controls.Add(addShapeForm);
                addShapeForm.Show();
            });
            formFactory.RegisterForm("Edit Form", () =>
            {
                panelProperties.Controls.Clear();
                EditShapePropertiesForm editShapePropertiesForm = new EditShapePropertiesForm(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    BackColor = panelProperties.BackColor
                };
                panelProperties.Controls.Add(editShapePropertiesForm);
                editShapePropertiesForm.Show();
            });
            formFactory.RegisterForm("Properties Form", () =>
            {
                panelProperties.Controls.Clear();
                shapePropertiesForm = new ShapePropertiesForm(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    BackColor = panelProperties.BackColor
                };
                panelProperties.Controls.Add(shapePropertiesForm);
                shapePropertiesForm.Show();
            });
            formFactory.RegisterForm("Shapes Info Form", () =>
            {
                panelProperties.Controls.Clear();
                ShapesInfoForm shapeListForm = new ShapesInfoForm(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    BackColor = panelProperties.BackColor
                };
                panelProperties.Controls.Add(shapeListForm);
                shapeListForm.Show();
            });

            new EventBinder(this);
        }

    }
}
