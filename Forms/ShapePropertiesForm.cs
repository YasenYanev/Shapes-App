using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Coursework.Shapes;
using Coursework.Interfaces;
namespace Coursework.Forms
{
    public partial class ShapePropertiesForm : Form
    {
        private Form1 mainForm;
        private IShape selectedShape;
        private Label lblX;
        private Label lblY;
        private Label lblWidth;
        private Label lblHeight;
        private Label lblArea;
        private Label lblPerimeter;
        private Label lblSpecificProperty;

        public ShapePropertiesForm(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.selectedShape = this.mainForm.shapeManager.selectedShape;
            DisplayShapeProperties();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ShapePropertiesForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ShapePropertiesForm";
            this.Text = "Shape Properties";
            this.ResumeLayout(false);
        }

        private void DisplayShapeProperties()
        {
            int verticalOffset = 30;
            int labelSpacing = 36;
            int centerX = (this.Width - 200) / 2;

            lblX = new Label
            {
                Text = "X: " + selectedShape.X,
                Location = new Point(centerX, verticalOffset),
                AutoSize = true
            };
            lblY = new Label
            {
                Text = "Y: " + selectedShape.Y,
                Location = new Point(centerX, verticalOffset + labelSpacing),
                AutoSize = true
            };
            verticalOffset += labelSpacing;

            if (selectedShape is Triangle triangle)
            {
                lblSpecificProperty = new Label
                {
                    Text = "Side Length: " + triangle.SideLength,
                    Location = new Point(centerX, verticalOffset + labelSpacing),
                    AutoSize = true
                };
                this.Controls.Add(lblSpecificProperty);
                verticalOffset += labelSpacing;
            }
            else if (selectedShape is Circle circle)
            {
                lblSpecificProperty = new Label
                {
                    Text = "Radius: " + circle.Radius,
                    Location = new Point(centerX, verticalOffset + labelSpacing),
                    AutoSize = true
                };
                this.Controls.Add(lblSpecificProperty);
                verticalOffset += labelSpacing;
            }

            lblWidth = new Label
            {
                Text = "Width: " + selectedShape.Width,
                Location = new Point(centerX, verticalOffset + labelSpacing),
                AutoSize = true
            };
            lblHeight = new Label
            {
                Text = "Height: " + selectedShape.Height,
                Location = new Point(centerX, verticalOffset + 2 * labelSpacing),
                AutoSize = true
            };
            lblPerimeter = new Label
            {
                Text = "Perimeter: " + selectedShape.CalculatePerimeter(),
                Location = new Point(centerX, verticalOffset + 3 * labelSpacing),
                AutoSize = true
            };
            lblArea = new Label
            {
                Text = "Area: " + selectedShape.CalculateArea(),
                Location = new Point(centerX, verticalOffset + 4 * labelSpacing),
                AutoSize = true
            };

            this.Controls.Add(lblX);
            this.Controls.Add(lblY);
            this.Controls.Add(lblWidth);
            this.Controls.Add(lblHeight);
            this.Controls.Add(lblPerimeter);
            this.Controls.Add(lblArea);
        }

        public void UpdateShapeProperties(int x, int y)
        {
            lblX.Text = "X: " + x;
            lblY.Text = "Y: " + y;
        }
    }
}
