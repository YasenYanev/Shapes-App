using System.Collections.Generic;
using System.Linq;
using Coursework.Shapes;

namespace Coursework.Forms
{
    public partial class AddShapeForm : Form
    {
        private Color selectedInnerColor = Color.Transparent;
        private Color selectedBorderColor = Color.Transparent;
        private MainForm mainform;
        private ComboBox comboBox1;
        private Label label1;
        private Button submitButton;
        private Label innerColorLabel;
        private Panel innerColorPanel;
        private Label borderColorLabel;
        private Panel borderColorPanel;

        public AddShapeForm(MainForm mainform)
        {
            this.mainform = mainform;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AddShapeForm
            // 
            this.ClientSize = new System.Drawing.Size(260, 540);
            this.Name = "AddShapeForm";
            this.Text = "Add Shape";
            this.ResumeLayout(false);

            comboBox1 = new ComboBox
            {
                Location = new Point(100, 43),
                Size = new Size(140, 23)
            };
            comboBox1.Items.AddRange(new object[] { "Triangle", "Circle", "Square", "Rectangle" });
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            label1 = new Label
            {
                Location = new Point(10, 46),
                Size = new Size(85, 15),
                Text = "Choose Shape:"
            };

            innerColorLabel = new Label
            {
                Location = new Point(10, 240),
                AutoSize = true,
                Text = "Choose Inner Color:",
                Visible = false
            };

            innerColorPanel = new Panel
            {
                Location = new Point(150, 240),
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Cursor = Cursors.Hand
            };
            innerColorPanel.Click += innerColorPanel_Click;

            borderColorLabel = new Label
            {
                Location = new Point(10, 280),
                AutoSize = true,
                Text = "Choose Border Color:",
                Visible = false
            };

            borderColorPanel = new Panel
            {
                Location = new Point(150, 280),
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Cursor = Cursors.Hand
            };
            borderColorPanel.Click += borderColorPanel_Click;

            submitButton = new Button
            {
                Location = new Point(95, 320),
                Size = new Size(75, 23),
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
            BackColor = Color.LightSteelBlue;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetColors();
            Controls.Clear();
            Controls.AddRange(new Control[] { label1, comboBox1 });

            string[] labels;
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Triangle":
                    labels = new[] { "Side Length" };
                    break;
                case "Circle":
                    labels = new[] { "Radius" };
                    break;
                case "Square":
                    labels = new[] { "Side Length" };
                    break;
                case "Rectangle":
                    labels = new[] { "Width", "Height" };
                    break;
                default:
                    labels = new string[] { };
                    break;
            }

            AddShapeControls(labels);

            int yOffset = 80 + labels.Length * 40;
            SetControlLocations(yOffset);

            innerColorLabel.Visible = innerColorPanel.Visible = borderColorLabel.Visible = borderColorPanel.Visible = submitButton.Visible = false;
            Controls.AddRange(new Control[] { innerColorLabel, innerColorPanel, borderColorLabel, borderColorPanel, submitButton });
        }

        private void ResetColors()
        {
            selectedInnerColor = Color.Transparent;
            selectedBorderColor = Color.Transparent;
            innerColorPanel.BackColor = Color.Transparent;
            borderColorPanel.BackColor = Color.Transparent;
        }

        private void AddShapeControls(string[] labels)
        {
            int yOffset = 80;
            foreach (var label in labels)
            {
                Controls.Add(new Label { Text = label + ":", Location = new Point(10, yOffset + 5), AutoSize = true });
                var txtBox = new TextBox { Location = new Point(100, yOffset), Size = new Size(140, 23), Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point), BackColor = Color.White, ForeColor = Color.Black };
                txtBox.TextChanged += TextBox_TextChanged;
                Controls.Add(txtBox);
                yOffset += 40;
            }
        }

        private void SetControlLocations(int yOffset)
        {
            innerColorLabel.Location = new Point(10, yOffset);
            innerColorPanel.Location = new Point(150, yOffset);
            borderColorLabel.Location = new Point(10, yOffset + 40);
            borderColorPanel.Location = new Point(150, yOffset + 40);
            submitButton.Location = new Point(95, yOffset + 80);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            bool allFieldsFilled = Controls.OfType<TextBox>().All(txtBox => !string.IsNullOrWhiteSpace(txtBox.Text));
            innerColorLabel.Visible = innerColorPanel.Visible = allFieldsFilled;
            submitButton.Visible = allFieldsFilled && selectedInnerColor != Color.Transparent && selectedBorderColor != Color.Transparent;
        }

        private void innerColorPanel_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedInnerColor = colorDialog.Color;
                    innerColorPanel.BackColor = selectedInnerColor;
                    borderColorLabel.Visible = borderColorPanel.Visible = true;
                    submitButton.Visible = selectedInnerColor != Color.Transparent && selectedBorderColor != Color.Transparent;
                }
            }
        }

        private void borderColorPanel_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedBorderColor = colorDialog.Color;
                    borderColorPanel.BackColor = selectedBorderColor;
                    submitButton.Visible = selectedInnerColor != Color.Transparent && selectedBorderColor != Color.Transparent;
                }
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            var shapeType = comboBox1.SelectedItem.ToString();
            var textBoxValues = new List<int>();

            foreach (var textBox in Controls.OfType<TextBox>())
                if (int.TryParse(textBox.Text, out int value))
                    textBoxValues.Add(value);

            mainform.shapeManager.AddShape(shapeType, textBoxValues, selectedInnerColor, selectedBorderColor);
        }
    }
}