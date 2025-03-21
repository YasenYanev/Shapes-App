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

        public EditShapePropertiesForm(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.selectedShape = this.mainForm.selectedShape;
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
            int labelSpacing = 36;
            int centerX = (this.ClientSize.Width - 200) / 2;

            // Specific property
            if (selectedShape is Triangle triangle)
            {
                this.Controls.Add(new Label { Text = "Side Length:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                txtSpecificProperty = new TextBox { Text = triangle.SideLength.ToString(), Location = new Point(centerX + 70, verticalOffset), Width = 100 };
                this.Controls.Add(txtSpecificProperty);
                verticalOffset += labelSpacing;
            }
            else if (selectedShape is Circle circle)
            {
                this.Controls.Add(new Label { Text = "Radius:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                txtSpecificProperty = new TextBox { Text = circle.Radius.ToString(), Location = new Point(centerX + 50, verticalOffset), Width = 100 };
                this.Controls.Add(txtSpecificProperty);
                verticalOffset += labelSpacing;
            }
            else if (selectedShape is Square square)
            {
                this.Controls.Add(new Label { Text = "Side Length:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                txtSpecificProperty = new TextBox { Text = square.Width.ToString(), Location = new Point(centerX + 70, verticalOffset), Width = 100 };
                this.Controls.Add(txtSpecificProperty);
                verticalOffset += labelSpacing;
            }
            else
            {
                // Width
                this.Controls.Add(new Label { Text = "Width:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                TextBox txtWidth = new TextBox { Text = selectedShape.Width.ToString(), Location = new Point(centerX + 70, verticalOffset), Width = 100 };
                this.Controls.Add(txtWidth);
                verticalOffset += labelSpacing;

                // Height
                this.Controls.Add(new Label { Text = "Height:", Location = new Point(centerX, verticalOffset), AutoSize = true });
                TextBox txtHeight = new TextBox { Text = selectedShape.Height.ToString(), Location = new Point(centerX + 70, verticalOffset), Width = 100 };
                this.Controls.Add(txtHeight);
                verticalOffset += labelSpacing;
            }

            btnSave = new Button { Text = "Save", Location = new Point(centerX + 50, verticalOffset + labelSpacing), Width = 100 };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Add updating of shape properties here

            // Refresh the canvas
            mainForm.panelCanvas.Refresh();
            this.Close();
        }
    }
}