using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Coursework.Shapes;
using Coursework.Interfaces;

namespace Coursework.Forms
{
    public partial class ShapesInfoForm : Form
    {
        private ShapeManager _shapeManager;

        public ShapesInfoForm(Form1 mainForm)
        {
            InitializeComponent();

            this._shapeManager = mainForm.shapeManager;

            var totalShapesArea = (from shape in _shapeManager.shapesList
                                   select shape.CalculateArea()).Sum();

            var highestPerimeterShape = (from shape in _shapeManager.shapesList
                                         orderby shape.CalculatePerimeter() descending
                                         select shape).First();

            var brightestInnerColorShape = _shapeManager.shapesList
                .OrderByDescending(shape => 0.299 * shape.InnerColor.R + 0.587 * shape.InnerColor.G + 0.114 * shape.InnerColor.B)
                .First();

            var brightestBorderColorShape = _shapeManager.shapesList
                .OrderByDescending(shape => 0.299 * shape.BorderColor.R + 0.587 * shape.BorderColor.G + 0.114 * shape.BorderColor.B)
                .First();

            var totalAreaLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Text = $"Total Area Of All Shapes: {totalShapesArea}",
                Dock = DockStyle.Top,
                Padding = new Padding(10)
            };

            var highestPerimeterLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Text = $"Highest Perimeter Of A Shape: {highestPerimeterShape.CalculatePerimeter()}",
                Dock = DockStyle.Top,
                Padding = new Padding(10)
            };

            var brightestInnerColorLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Text = $"Shape With Brithest Inner Color:\n {brightestInnerColorShape.GetType().Name}",
                Dock = DockStyle.Top,
                Padding = new Padding(10)
            };

            var brightestBorderColorLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Text = $"Shape With Brithest Border Color:\n {brightestBorderColorShape.GetType().Name}",
                Dock = DockStyle.Top,
                Padding = new Padding(10)
            };

            this.Controls.Add(brightestBorderColorLabel);
            this.Controls.Add(brightestInnerColorLabel);
            this.Controls.Add(highestPerimeterLabel);
            this.Controls.Add(totalAreaLabel);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //  
            // ShapesInfoForm  
            //  
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Name = "ShapesInfoForm";
            this.Text = "Shapes Information";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }
    }
}