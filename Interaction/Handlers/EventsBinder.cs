using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coursework.Forms;
using Coursework.Interfaces;
using Coursework.Shapes;
using Newtonsoft.Json;


namespace Coursework.Interaction.Handlers
{
    public class EventsBinder
    {
        private Form1 _mainForm;
        private ShapeManager _shapeManager;
        private ShapeInteractionHandler _shapeInteractionHandler;
        public EventsBinder(Form1 form) {
            _mainForm = form;
            _shapeManager = form.shapeManager;
            _shapeInteractionHandler = form.shapeInteractionHandler;
            BindEvents();
        }

        public void BindEvents()
        {
            _mainForm.panelCanvas.MouseClick += _shapeInteractionHandler.OnMouseClick;
            _mainForm.panelCanvas.MouseDown += _shapeInteractionHandler.OnMouseDown;
            _mainForm.panelCanvas.MouseMove += _shapeInteractionHandler.OnMouseMove;

            _mainForm.addShapeBtn.Click += ShowAddFormBtn;
            _mainForm.editShapeBtn.Click += ShowEditFormBtn;
            _mainForm.deleteShapeBtn.Click += DeleteShapeBtn;
            _mainForm.clearBtn.Click += ClearShapesBtn;
            _mainForm.saveBtn.Click += SaveShapesBtn;
            _mainForm.loadBtn.Click += ImportShapesBtn;
            _mainForm.shapesInfoBtn.Click += ShapesInfo;
        }
        private void ShowAddFormBtn(object sender, EventArgs e)
        {
            if (_shapeManager.selectedShape != null)
            {
                _shapeManager.selectedShape.IsSelected = false;
                _shapeManager.selectedShape = null;
                _mainForm.panelCanvas.Refresh();
            }

            _mainForm.createAddShapeForm();
        }

        private void ShowEditFormBtn(object sender, EventArgs e)
        {
            if (_shapeManager.selectedShape == null)
            {
                MessageBox.Show("No shape selected");
                return;
            }

            _mainForm.createEditPopretiesForm();

        }

        private void DeleteShapeBtn(object sender, EventArgs e)
        {
            if (_shapeManager.selectedShape == null)
            {
                MessageBox.Show("No shape selected");
                return;
            }

            _shapeManager.DeleteShape(_shapeManager.selectedShape);
            _shapeManager.selectedShape = null;

            _mainForm.panelCanvas.Refresh();
            _mainForm.panelProperties.Controls.Clear();
        }

        private void ClearShapesBtn(object sender, EventArgs e)
        {
            for (int i = _shapeManager.shapesList.Count - 1; i >= 0; i--)
                _shapeManager.DeleteShape(_shapeManager.shapesList[i]);

            _mainForm.panelProperties.Controls.Clear();
            _mainForm.panelCanvas.Refresh();
        }

        private void SaveShapesBtn(object sender, EventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(_shapeManager.shapesList, Formatting.Indented,
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


        private void ImportShapesBtn(object sender, EventArgs e)
        {
            string jsonString;

            foreach (Shape shape in _shapeManager.shapesList)
                _mainForm.panelCanvas.Paint -= shape.OnPaint;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    jsonString = File.ReadAllText(openFileDialog.FileName);
                    var deserializedShapes = JsonConvert.DeserializeObject<List<IShape>>(jsonString,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                    // Cast the deserialized shapes to the Shape type
                    _shapeManager.shapesList = deserializedShapes.Cast<Shape>().ToList();
                }
            }

            foreach (Shape shape in _shapeManager.shapesList)
                _mainForm.panelCanvas.Paint += shape.OnPaint;

            _mainForm.panelProperties.Controls.Clear();
            _mainForm.panelCanvas.Refresh();
        }

        private void ShapesInfo(object sender, EventArgs e)
        {
            if(_shapeManager.shapesList.Count() < 1)
            {
                MessageBox.Show("Add a shape");
                return;
            }

            _mainForm.createShapesInfoForm();
        }
    }
}
