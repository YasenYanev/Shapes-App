using Coursework.Shapes;
using Coursework.Controls.Events;
using System.Windows.Forms;
using Coursework.Forms;
using Coursework.Interfaces;
using System.Xml.Serialization;
using System;
using System.Diagnostics;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Serialization;

namespace Coursework
{
    public partial class Form1 : Form
    {
        public MouseHandler mouseHandler;
        public List<Shape> shapesList = new List<Shape> { };
        public Shape? selectedShape = null;
        public ShapePropertiesForm shapePropertiesForm;
        public Form1()
        {
            InitializeComponent();
            mouseHandler = new MouseHandler(this);
            panelCanvas.MouseClick += mouseHandler.OnMouseClick;
            panelCanvas.MouseDown += mouseHandler.OnMouseDown;
            panelCanvas.MouseMove += mouseHandler.OnMouseMove;

            this.MinimumSize = new Size(850, 650);
        }

        public void OnTryShapeSelect(Shape? shape)
        {
            if (shape != null && shape == selectedShape) return;
            if (selectedShape != null) selectedShape.IsSelected = false;
            if (shape == null)
            {
                selectedShape = null;
                panelProperties.Controls.Clear();
                panelCanvas.Refresh();
                return;
            }

            selectedShape = shape;
            selectedShape.IsSelected = true;


            createPropretiesForm();
            panelCanvas.Refresh();
        }
        private void AddShapeBtn_Click(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                selectedShape.IsSelected = false;
                selectedShape = null;
                panelCanvas.Refresh();
            }

            createAddShapeForm();
        }

        private void editShapeBtn_Click(object sender, EventArgs e)
        {
            if (selectedShape == null)
            {
                MessageBox.Show("No shape selected");
                return;
            }

            createEditPopretiesForm();

        }

        private void deleteShapeBtn_Click(object sender, EventArgs e)
        {
            if (selectedShape == null)
            {
                MessageBox.Show("No shape selected");
                return;
            }

            panelCanvas.Paint -= selectedShape.OnPaint;
            shapesList.Remove(selectedShape);

            selectedShape = null;

            panelCanvas.Refresh();
            panelProperties.Controls.Clear();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            for (int i = shapesList.Count - 1; i >= 0; i--)
                panelCanvas.Paint -= shapesList[i].OnPaint;

            shapesList.Clear();
            panelProperties.Controls.Clear();
            panelCanvas.Refresh();
        }

        private void createEditPopretiesForm()
        {
            panelProperties.Controls.Clear();
            EditShapePropertiesForm editShapePropertiesForm = new EditShapePropertiesForm(this);
            editShapePropertiesForm.TopLevel = false;
            editShapePropertiesForm.FormBorderStyle = FormBorderStyle.None;
            editShapePropertiesForm.Dock = DockStyle.Fill;
            editShapePropertiesForm.BackColor = panelProperties.BackColor;
            panelProperties.Controls.Add(editShapePropertiesForm);
            editShapePropertiesForm.Show();
        }

        public void createPropretiesForm()
        {
            panelProperties.Controls.Clear();
            shapePropertiesForm = new ShapePropertiesForm(this);
            shapePropertiesForm.TopLevel = false;
            shapePropertiesForm.FormBorderStyle = FormBorderStyle.None;
            shapePropertiesForm.Dock = DockStyle.Fill;
            shapePropertiesForm.BackColor = panelProperties.BackColor;
            panelProperties.Controls.Add(shapePropertiesForm);
            shapePropertiesForm.Show();
        }
        private void createAddShapeForm()
        {
            panelProperties.Controls.Clear();
            AddShapeForm addShapeForm = new AddShapeForm(this);
            addShapeForm.TopLevel = false;
            addShapeForm.FormBorderStyle = FormBorderStyle.None;
            addShapeForm.Dock = DockStyle.Fill;
            addShapeForm.BackColor = panelProperties.BackColor;
            panelProperties.Controls.Add(addShapeForm);
            addShapeForm.Show();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(shapesList, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,  // Prevents reference loops
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects, // Keeps references intact
                    NullValueHandling = NullValueHandling.Include,  // Include null values
                });

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(Path.Combine(folderDialog.SelectedPath, "shapes.json"), jsonString);
            }
        }


        private void loadBtn_Click(object sender, EventArgs e)
        {
            string jsonString;

            foreach(Shape shape in shapesList)
                panelCanvas.Paint -= shape.OnPaint;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    jsonString = File.ReadAllText(openFileDialog.FileName);
                    shapesList = JsonConvert.DeserializeObject<List<Shape>>(jsonString,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });
                }
            }

            foreach (Shape shape in shapesList)
                panelCanvas.Paint += shape.OnPaint;

            panelProperties.Controls.Clear();
            panelCanvas.Refresh();
        }
    }
}
