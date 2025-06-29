using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Coursework.Interfaces;
using Coursework.Shapes.Types;
using Coursework.Utilities;

namespace Coursework.Forms
{
    public partial class EditShapePropertiesForm : Form
    {
        private MainForm _mainForm;
        private IShape selectedShape;

        private TextBox txtSpecificProperty;
        private TextBox txtWidth;
        private TextBox txtHeight;
        private Button btnSave;

        private Label innerColorLabel;
        private Panel innerColorPanel;
        private Label borderColorLabel;
        private Panel borderColorPanel;

        private Color selectedInnerColor;
        private Color selectedBorderColor;

        private int y;
        private int spacing;
        private int centerX;
        private int inputX;

        public EditShapePropertiesForm(MainForm mainForm)
        {
            _mainForm = mainForm;
            selectedShape = _mainForm.selectedShape;
            selectedInnerColor = selectedShape.InnerColor;
            selectedBorderColor = selectedShape.BorderColor;


            y = 30;
            spacing = 36;
            centerX = (ClientSize.Width - 200) / 2;
            inputX = centerX + 90;


            InitializeComponent();
            DisplayShapeProperties();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ClientSize = new Size(284, 261);
            Name = "Edit Shape Properties";
            Text = "Edit Shape Properties";
            ResumeLayout(false);
        }

        private void DisplayShapeProperties()
        {

            if (selectedShape is Triangle triangle)
            {
                var label = UIHelper.CreateLabel("Side Length:", centerX, y);
                txtWidth = UIHelper.CreateTextbox(triangle.SideLength.ToString(), inputX, y);
                Controls.Add(label);
                Controls.Add(txtWidth);
                y += spacing;
                txtHeight = null;
            }
            else if (selectedShape is Circle circle)
            {
                var label = UIHelper.CreateLabel("Radius:", centerX, y);
                txtWidth = UIHelper.CreateTextbox(circle.Radius.ToString(), inputX, y);
                Controls.Add(label);
                Controls.Add(txtWidth);
                y += spacing;
                txtHeight = null;
            }
            else if (selectedShape.Width == selectedShape.Height)
            {
                var label = UIHelper.CreateLabel("Side Length:", centerX, y);
                txtWidth = UIHelper.CreateTextbox(selectedShape.Width.ToString(), inputX, y);
                Controls.Add(label);
                Controls.Add(txtWidth);
                y += spacing;
                txtHeight = null;
            }
            else
            {
                var labelW = UIHelper.CreateLabel("Width:", centerX, y);
                txtWidth = UIHelper.CreateTextbox(selectedShape.Width.ToString(), inputX, y);
                Controls.Add(labelW);
                Controls.Add(txtWidth);
                y += spacing;

                var labelH = UIHelper.CreateLabel("Height:", centerX, y);
                txtHeight = UIHelper.CreateTextbox(selectedShape.Height.ToString(), inputX, y);
                Controls.Add(labelH);
                Controls.Add(txtHeight);
                y += spacing;
            }

            innerColorLabel = UIHelper.CreateLabel("Choose Inner Color:", centerX, y);
            innerColorPanel = UIHelper.CreateColorPanelFromColors(new Point(centerX + 150, y), selectedInnerColor, InnerColorPanel_Click);
            y += spacing;

            borderColorLabel = UIHelper.CreateLabel("Choose Border Color:", centerX, y);
            borderColorPanel = UIHelper.CreateColorPanelFromColors(new Point(centerX + 150, y), selectedBorderColor, BorderColorPanel_Click);
            y += spacing;

            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(centerX + 50, y + spacing),
                Width = 100
            };
            btnSave.Click += BtnSave_Click;

            Controls.AddRange(new Control[]
            {
                innerColorLabel,
                innerColorPanel,
                borderColorLabel,
                borderColorPanel,
                btnSave
            });
        }

        private void InnerColorPanel_Click(object sender, EventArgs e)
        {
            using var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedInnerColor = colorDialog.Color;
                innerColorPanel.BackColor = selectedInnerColor;
            }
        }

        private void BorderColorPanel_Click(object sender, EventArgs e)
        {
            using var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedBorderColor = colorDialog.Color;
                borderColorPanel.BackColor = selectedBorderColor;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var values = Controls.OfType<TextBox>()
                                 .Select(txt => int.TryParse(txt.Text, out int val) ? val : (int?)null)
                                 .Where(val => val.HasValue)
                                 .Select(val => val.Value)
                                 .ToList();

            _mainForm.operationFactory.GetOperationByName("Edit").Execute(values, selectedInnerColor, selectedBorderColor);
            _mainForm.panelCanvas.Refresh();
            _mainForm.formFactory.CreateForm("Properties Form");
            Close();
        }
    }
}
