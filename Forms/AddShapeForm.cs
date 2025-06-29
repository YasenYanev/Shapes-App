using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Coursework.Shapes;
using Coursework.Utilities;

namespace Coursework.Forms
{
    public partial class AddShapeForm : Form
    {
        private Color selectedInnerColor = Color.Transparent;
        private Color selectedBorderColor = Color.Transparent;
        private MainForm _mainform;

        private ComboBox comboBox1;
        private Label label1;
        private Button submitButton;
        private Label innerColorLabel;
        private Panel innerColorPanel;
        private Label borderColorLabel;
        private Panel borderColorPanel;

        private int y;
        private int spacing;
        private int centerX;
        private int inputX;

        public AddShapeForm(MainForm mainform)
        {
            _mainform = mainform;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            ClientSize = new Size(284, 540);
            Name = "Add Shape Form";
            Text = "Add Shape";
            BackColor = Color.LightSteelBlue;

            y = 30;
            spacing = 36;
            centerX = (ClientSize.Width - 200) / 2;
            inputX = centerX + 90;

            // Shape selector
            label1 = UIHelper.CreateLabel("Choose Shape:", centerX, y);
            comboBox1 = new ComboBox
            {
                Location = new Point(inputX, y - 3),
                Size = new Size(100, 23)
            };
            comboBox1.Items.AddRange(new object[] { "Triangle", "Circle", "Square", "Rectangle" });
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            y += spacing;

            // Color controls - initially invisible
            innerColorLabel = UIHelper.CreateLabel("Choose Inner Color:", centerX, y, visable: false);
            innerColorPanel = UIHelper.CreateColorPanel(inputX, y);
            innerColorPanel.Visible = false;
            innerColorPanel.Click += innerColorPanel_Click;
            y += spacing;

            borderColorLabel = UIHelper.CreateLabel("Choose Border Color:", centerX, y, visable: false);
            borderColorPanel = UIHelper.CreateColorPanel(inputX, y);
            borderColorPanel.Visible = false;
            borderColorPanel.Click += borderColorPanel_Click;
            y += spacing;

            submitButton = new Button
            {
                Location = new Point(centerX + 50, y),
                Size = new Size(100, 28),
                Text = "Submit",
                Visible = false
            };
            submitButton.Click += submitButton_Click;

            Controls.AddRange(new Control[]
            {
                label1,
                comboBox1,
                innerColorLabel,
                innerColorPanel,
                borderColorLabel,
                borderColorPanel,
                submitButton
            });

            ResumeLayout(false);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Remove previous textboxes and labels (except the selector and colors)
            foreach (Control c in Controls.OfType<Label>().ToList())
            {
                if (c != label1 && c != innerColorLabel && c != borderColorLabel)
                    Controls.Remove(c);
            }

            foreach (Control c in Controls.OfType<TextBox>().ToList())
            {
                Controls.Remove(c);
            }

            ResetColors();

            y = spacing * 2;

            string[] labels = comboBox1.SelectedItem.ToString() switch
            {
                "Triangle" => new[] { "Side Length" },
                "Circle" => new[] { "Radius" },
                "Square" => new[] { "Side Length" },
                "Rectangle" => new[] { "Width", "Height" },
                _ => new string[] { }
            };

            foreach (var labelText in labels)
            {
                var label = UIHelper.CreateLabel(labelText + ":", centerX, y);
                var textBox = UIHelper.CreateTextbox("", inputX, y);
                textBox.TextChanged += TextBox_TextChanged;
                Controls.Add(label);
                Controls.Add(textBox);
                y += spacing;
            }

            // Reposition and add color selectors and submit button
            innerColorLabel.Location = new Point(centerX, y);
            innerColorPanel.Location = new Point(inputX + 60, y);
            y += spacing;

            borderColorLabel.Location = new Point(centerX, y);
            borderColorPanel.Location = new Point(inputX + 60, y);
            y += spacing;

            submitButton.Location = new Point(centerX + 50, y + spacing);

            Controls.AddRange(new Control[]
            {
                innerColorLabel,
                innerColorPanel,
                borderColorLabel,
                borderColorPanel,
                submitButton
            });

            innerColorLabel.Visible = innerColorPanel.Visible =
            borderColorLabel.Visible = borderColorPanel.Visible =
            submitButton.Visible = false;
        }

        private void ResetColors()
        {
            selectedInnerColor = Color.Transparent;
            selectedBorderColor = Color.Transparent;
            innerColorPanel.BackColor = Color.Transparent;
            borderColorPanel.BackColor = Color.Transparent;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            bool allFilled = Controls.OfType<TextBox>().All(txt => !string.IsNullOrWhiteSpace(txt.Text));

            innerColorLabel.Visible = innerColorPanel.Visible = allFilled;

            // Button only visible if all fields and colors are set
            submitButton.Visible = allFilled && selectedInnerColor != Color.Transparent && selectedBorderColor != Color.Transparent;
        }

        private void innerColorPanel_Click(object sender, EventArgs e)
        {
            using var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedInnerColor = colorDialog.Color;
                innerColorPanel.BackColor = selectedInnerColor;
                borderColorLabel.Visible = borderColorPanel.Visible = true;

                // Enable button if both colors are selected
                submitButton.Visible = Controls.OfType<TextBox>().All(txt => !string.IsNullOrWhiteSpace(txt.Text)) &&
                                       selectedBorderColor != Color.Transparent;
            }
        }

        private void borderColorPanel_Click(object sender, EventArgs e)
        {
            using var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedBorderColor = colorDialog.Color;
                borderColorPanel.BackColor = selectedBorderColor;

                // Enable button if both colors are selected
                submitButton.Visible = Controls.OfType<TextBox>().All(txt => !string.IsNullOrWhiteSpace(txt.Text)) &&
                                       selectedInnerColor != Color.Transparent;
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            string shapeType = comboBox1.SelectedItem.ToString();
            var textBoxValues = Controls.OfType<TextBox>()
                .Select(txt => int.TryParse(txt.Text, out int val) ? val : (int?)null)
                .Where(val => val.HasValue)
                .Select(val => val.Value)
                .ToList();

            _mainform.operationFactory.GetOperationByName("Add").Execute(shapeType, textBoxValues, selectedInnerColor, selectedBorderColor);
            _mainform.panelCanvas.Refresh();
            Close();
        }
    }
}
