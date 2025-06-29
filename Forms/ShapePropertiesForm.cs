using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Coursework.Interfaces;
using Coursework.Shapes.Types;
using Coursework.Utilities;

namespace Coursework.Forms
{
    public partial class ShapePropertiesForm : Form
    {
        private MainForm _mainForm;
        private IShape selectedShape;

        private Label lblX;
        private Label lblY;
        private Label lblWidth;
        private Label lblHeight;
        private Label lblArea;
        private Label lblPerimeter;
        private Label lblSpecificProperty;

        public ShapePropertiesForm(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            this.selectedShape = _mainForm.selectedShape;
            DisplayShapeProperties();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(284, 261);
            this.Name = "ShapePropertiesForm";
            this.Text = "Shape Properties";
            this.ResumeLayout(false);
        }

        private void DisplayShapeProperties()
        {
            if (selectedShape == null)
            {
                MessageBox.Show("No shape selected.");
                return;
            }

            int verticalOffset = 30;
            int labelSpacing = 36;
            int centerX = (this.ClientSize.Width - 200) / 2;

            lblX = UIHelper.CreateLabel($"X: {selectedShape.X}", centerX, verticalOffset);
            verticalOffset += labelSpacing;

            lblY = UIHelper.CreateLabel($"Y: {selectedShape.Y}", centerX, verticalOffset);
            verticalOffset += labelSpacing;

            if (selectedShape is Triangle triangle)
            {
                lblSpecificProperty = UIHelper.CreateLabel($"Side Length: {triangle.SideLength}", centerX, verticalOffset);
                verticalOffset += labelSpacing;
            }
            else if (selectedShape is Circle circle)
            {
                lblSpecificProperty = UIHelper.CreateLabel($"Radius: {circle.Radius}", centerX, verticalOffset);
                verticalOffset += labelSpacing;
            }

            lblWidth = UIHelper.CreateLabel($"Width: {selectedShape.Width}", centerX, verticalOffset);
            verticalOffset += labelSpacing;

            lblHeight = UIHelper.CreateLabel($"Height: {selectedShape.Height}", centerX, verticalOffset);
            verticalOffset += labelSpacing;

            lblPerimeter = UIHelper.CreateLabel($"Perimeter: {selectedShape.CalculatePerimeter()}", centerX, verticalOffset);
            verticalOffset += labelSpacing;

            lblArea = UIHelper.CreateLabel($"Area: {selectedShape.CalculateArea()}", centerX, verticalOffset);

            var controlsToAdd = new System.Collections.Generic.List<Control> { lblX, lblY, lblWidth, lblHeight, lblPerimeter, lblArea };
            if (lblSpecificProperty != null) controlsToAdd.Add(lblSpecificProperty);

            Controls.AddRange(controlsToAdd.ToArray());
        }

        public void UpdateShapePositionOnForm(int x, int y)
        {
            lblX.Text = $"X: {x}";
            lblY.Text = $"Y: {y}";
        }
    }
}
