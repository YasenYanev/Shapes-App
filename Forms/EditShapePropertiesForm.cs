using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Coursework.Shapes;

namespace Coursework.Forms
{
    public partial class EditShapePropertiesForm : Form
    {
        private Form1 mainForm;
        private Shape selectedShape;
        private TextBox txtSpecificProperty;
        private Button btnSave;
        private Label colorLabel;
        private Panel innerColorPanel;
        private Label borderColorLabel;
        private Panel borderColorPanel;
        private Color selectedInnerColor;
        private Color selectedBorderColor;
        private TextBox txtWidth;
        private TextBox txtHeight;

        public EditShapePropertiesForm(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.selectedShape = this.mainForm.selectedShape;
            this.selectedInnerColor = selectedShape.InnerColor;
            this.selectedBorderColor = selectedShape.BorderColor;
            DisplayShapeProperties();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // EditShapePropertiesForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Edit Shape Properties";
            this.Text = "Edit Shape Properties";
            this.ResumeLayout(false);
        }

        private void DisplayShapeProperties()
        {
            int verticalOffset = 30;
            int labelSpacing = 36; // Set to match other forms
            int centerX = (this.ClientSize.Width - 200) / 2;
            int horizontalSpacing = 10; // Shortened horizontal spacing

            // Specific property
            if (selectedShape is Triangle triangle)
            {
                this.Controls.Add(new Label { Text = "Side Length:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                txtSpecificProperty = new TextBox { Text = triangle.SideLength.ToString(), Location = new Point(centerX + 80 + horizontalSpacing, verticalOffset), Width = 100 };
                this.Controls.Add(txtSpecificProperty);
                verticalOffset += labelSpacing;
            }
            else if (selectedShape is Circle circle)
            {
                this.Controls.Add(new Label { Text = "Radius:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                txtSpecificProperty = new TextBox { Text = circle.Radius.ToString(), Location = new Point(centerX + 80 + horizontalSpacing, verticalOffset), Width = 100 };
                this.Controls.Add(txtSpecificProperty);
                verticalOffset += labelSpacing;
            }
            else if (selectedShape.Width == selectedShape.Height)
            {
                this.Controls.Add(new Label { Text = "Side Length:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                txtSpecificProperty = new TextBox { Text = selectedShape.Width.ToString(), Location = new Point(centerX + 80 + horizontalSpacing, verticalOffset), Width = 100 };
                this.Controls.Add(txtSpecificProperty);
                verticalOffset += labelSpacing;
            }
            else
            {
                // Width
                this.Controls.Add(new Label { Text = "Width:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                txtWidth = new TextBox { Text = selectedShape.Width.ToString(), Location = new Point(centerX + 80 + horizontalSpacing, verticalOffset), Width = 100 };
                this.Controls.Add(txtWidth);
                verticalOffset += labelSpacing;

                // Height
                this.Controls.Add(new Label { Text = "Height:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                txtHeight = new TextBox { Text = selectedShape.Height.ToString(), Location = new Point(centerX + 80 + horizontalSpacing, verticalOffset), Width = 100 };
                this.Controls.Add(txtHeight);
                verticalOffset += labelSpacing;
            }

            // Color selection
            colorLabel = new Label { Text = "Choose Inner Color:", Location = new Point(centerX, verticalOffset), AutoSize = true };
            innerColorPanel = new Panel { Location = new Point(centerX + 150, verticalOffset), Size = new Size(20, 20), BackColor = selectedInnerColor, BorderStyle = BorderStyle.FixedSingle, Cursor = Cursors.Hand };
            innerColorPanel.Click += InnerColorPanel_Click;
            this.Controls.Add(colorLabel);
            this.Controls.Add(innerColorPanel);
            verticalOffset += labelSpacing;

            borderColorLabel = new Label { Text = "Choose Border Color:", Location = new Point(centerX, verticalOffset), AutoSize = true };
            borderColorPanel = new Panel { Location = new Point(centerX + 150, verticalOffset), Size = new Size(20, 20), BackColor = selectedBorderColor, BorderStyle = BorderStyle.FixedSingle, Cursor = Cursors.Hand };
            borderColorPanel.Click += BorderColorPanel_Click;
            this.Controls.Add(borderColorLabel);
            this.Controls.Add(borderColorPanel);
            verticalOffset += labelSpacing;

            // Save button
            btnSave = new Button { Text = "Save", Location = new Point(centerX + 50, verticalOffset + labelSpacing), Width = 100 };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
        }

        private void InnerColorPanel_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedInnerColor = colorDialog.Color;
                    innerColorPanel.BackColor = selectedInnerColor;
                }
            }
        }

        private void BorderColorPanel_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedBorderColor = colorDialog.Color;
                    borderColorPanel.BackColor = selectedBorderColor;
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Update shape properties
            if (selectedShape is Triangle) selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, int.Parse(txtSpecificProperty.Text));
            else if (selectedShape is Circle) selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, int.Parse(txtSpecificProperty.Text));
            else if (txtSpecificProperty != null) selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, int.Parse(txtSpecificProperty.Text), int.Parse(txtSpecificProperty.Text));
            else selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, int.Parse(txtWidth.Text), int.Parse(txtHeight.Text));

            // Refresh the canvas
            mainForm.panelCanvas.Refresh();
            mainForm.createPropretiesForm();
            this.Close();
        }
    }
}