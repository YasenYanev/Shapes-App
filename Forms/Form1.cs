using System.Windows.Forms;
using Coursework.Forms;
using System;
using Coursework.Shapes;
using Coursework.Interaction.Handlers;

namespace Coursework
{
    public partial class Form1 : Form
    {
        public ShapeManager shapeManager;
        public ShapeInteractionHandler shapeInteractionHandler;
        public AddShapeForm? addShapeForm;
        public ShapePropertiesForm? shapePropertiesForm;
        public EditShapePropertiesForm? editShapeForm;
        public ShapesInfoForm? shapesInfoForm;


        public Form1()
        {
            InitializeComponent();

            shapeManager = new ShapeManager(this);
            shapeInteractionHandler = new ShapeInteractionHandler(this);
            new EventsBinder(this);
            this.MinimumSize = new Size(650, 650);
        }
        public void createAddShapeForm()
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
        }
        public void createEditPopretiesForm()
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
        }

        public void createPropretiesForm()
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
        }

        public void createShapesInfoForm()
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
        }

    }
}
