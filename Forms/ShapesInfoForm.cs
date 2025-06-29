using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Coursework.Interfaces;
using Coursework.Operations.Base;
using Coursework.Utilities;

namespace Coursework.Forms
{
    public partial class ShapesInfoForm : Form
    {
        private MainForm _mainForm;

        private Label lblTotalArea;
        private Label lblHighestPerimeter;
        private Label lblBrightestInnerColor;
        private Label lblBrightestBorderColor;

        public ShapesInfoForm(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            DisplayShapeStatistics();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(400, 300);
            this.Name = "ShapesInfoForm";
            this.Text = "Shapes Information";
            this.ResumeLayout(false);
        }

        private void DisplayShapeStatistics()
        {
            int verticalOffset = 30;
            int labelSpacing = 40;
            int centerX = 20;

            var totalShapesArea = _mainForm.shapesList.Sum(shape => shape.CalculateArea());

            var highestPerimeterShape = _mainForm.shapesList
                .OrderByDescending(shape => shape.CalculatePerimeter())
                .FirstOrDefault();

            var brightestInnerColorShape = _mainForm.shapesList
                .OrderByDescending(shape => 0.299 * shape.InnerColor.R + 0.587 * shape.InnerColor.G + 0.114 * shape.InnerColor.B)
                .FirstOrDefault();

            var brightestBorderColorShape = _mainForm.shapesList
                .OrderByDescending(shape => 0.299 * shape.BorderColor.R + 0.587 * shape.BorderColor.G + 0.114 * shape.BorderColor.B)
                .FirstOrDefault();

            lblTotalArea = UIHelper.CreateLabel($"Total Area Of All Shapes: {totalShapesArea}", centerX, verticalOffset);
            verticalOffset += labelSpacing;

            lblHighestPerimeter = UIHelper.CreateLabel(
                $"Highest Perimeter Of A Shape: {highestPerimeterShape?.CalculatePerimeter()}", centerX, verticalOffset);
            verticalOffset += labelSpacing;

            lblBrightestInnerColor = UIHelper.CreateLabel(
                $"Shape With Brightest Inner Color:\n{brightestInnerColorShape?.GetType().Name}", centerX, verticalOffset);
            verticalOffset += labelSpacing;

            lblBrightestBorderColor = UIHelper.CreateLabel(
                $"Shape With Brightest Border Color:\n{brightestBorderColorShape?.GetType().Name}", centerX, verticalOffset);

            Controls.AddRange(new Control[]
            {
                lblTotalArea,
                lblHighestPerimeter,
                lblBrightestInnerColor,
                lblBrightestBorderColor
            });
        }
    }
}
