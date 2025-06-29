using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coursework.Forms;
using Coursework.Interfaces;
using Coursework.Shapes.Base;
using Coursework.Utilities;
using Newtonsoft.Json;


namespace Coursework.Interaction
{
    public class EventBinder
    {
        private MainForm _mainForm;
        private FormFactory _formsFactory;
        //
        private Point lastMousePosition = new Point(0, 0);
        //

        public EventBinder(MainForm form) {
            _mainForm = form;
            BindEvents();
        }

        public void BindEvents()
        {
            _mainForm.panelCanvas.MouseClick += OnMouseClickOrDown;
            _mainForm.panelCanvas.MouseDown += OnMouseClickOrDown;
            _mainForm.panelCanvas.MouseMove += OnMouseMove;

            _mainForm.addShapeBtn.Click += ShowAddFormBtn;
            _mainForm.editShapeBtn.Click += ShowEditFormBtn;
            _mainForm.deleteShapeBtn.Click += DeleteShapeBtn;
            _mainForm.clearBtn.Click += ClearShapesBtn;
            _mainForm.saveBtn.Click += SaveShapesBtn;
            _mainForm.loadBtn.Click += ImportShapesBtn;
            _mainForm.shapesInfoBtn.Click += ShapesInfo;
        }
        public void OnMouseClickOrDown(object sender, MouseEventArgs e)
        {
            Shape? shapeToSelect = null;
            foreach (Shape shape in _mainForm.shapesList)
                if (shape.IsMouseInside(e.X, e.Y))
                {
                    shapeToSelect = shape;
                    break;
                }

            _mainForm.operationFactory.GetOperationByName("Select")?.Execute(shapeToSelect);
            _mainForm.panelCanvas.Refresh();
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            bool cursorSet = false;

            if (e.Button == MouseButtons.Left && _mainForm.selectedShape != null && _mainForm.selectedShape.IsSelected)
            {
                _mainForm.selectedShape.UpdateLocation(lastMousePosition.X, lastMousePosition.Y, e.X, e.Y,
                    0, _mainForm.panelCanvas.Width, 0, _mainForm.panelCanvas.Height);
                _mainForm.Cursor = Cursors.SizeAll;
                _mainForm.shapePropertiesForm?.UpdateShapePositionOnForm(_mainForm.selectedShape.X, _mainForm.selectedShape.Y);
                cursorSet = true;
            }
            else
                foreach (Shape shape in _mainForm.shapesList)
                    if (shape.IsMouseInside(e.X, e.Y))
                    {
                        _mainForm.Cursor = Cursors.Hand;
                        cursorSet = true;
                        break;
                    }

            if (!cursorSet)
                _mainForm.Cursor = Cursors.Default;

            lastMousePosition.X = e.X;
            lastMousePosition.Y = e.Y;
            _mainForm.panelCanvas.Refresh();
        }

        private void ShowAddFormBtn(object sender, EventArgs e)
        {
            if (_mainForm.selectedShape != null)
            {
                _mainForm.selectedShape.IsSelected = false;
                _mainForm.selectedShape = null;
                _mainForm.panelCanvas.Refresh();
            }

            _mainForm.formFactory.CreateForm("Add Form");
        }

        private void ShowEditFormBtn(object sender, EventArgs e)
        {
            if (_mainForm.selectedShape == null)
            {
                MessageBox.Show("No shape selected");
                return;
            }

            _mainForm.formFactory.CreateForm("Edit Form");

        }

        private void DeleteShapeBtn(object sender, EventArgs e)
        {
            if (_mainForm.selectedShape == null)
            {
                MessageBox.Show("No shape selected");
                return;
            }

            _mainForm.operationFactory.GetOperationByName("Delete").Execute(_mainForm.selectedShape);
            _mainForm.selectedShape = null;

            _mainForm.panelCanvas.Refresh();
            _mainForm.panelProperties.Controls.Clear();
        }

        private void ClearShapesBtn(object sender, EventArgs e)
        {
            for (int i = _mainForm.shapesList.Count - 1; i >= 0; i--)
                _mainForm.operationFactory.GetOperationByName("Delete").Execute(_mainForm.shapesList[i]);

            _mainForm.panelProperties.Controls.Clear();
            _mainForm.panelCanvas.Refresh();
        }

        private void SaveShapesBtn(object sender, EventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(_mainForm.shapesList, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    NullValueHandling = NullValueHandling.Include,
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

            foreach (Shape shape in _mainForm.shapesList)
                _mainForm.panelCanvas.Paint -= shape.OnPaint;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    jsonString = File.ReadAllText(openFileDialog.FileName);

                    _mainForm.shapesList = JsonConvert.DeserializeObject<List<Shape>>(jsonString,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });
                }
            }

            foreach (Shape shape in _mainForm.shapesList)
                _mainForm.panelCanvas.Paint += shape.OnPaint;

            _mainForm.panelProperties.Controls.Clear();
            _mainForm.panelCanvas.Refresh();
        }

        private void ShapesInfo(object sender, EventArgs e)
        {
            if(_mainForm.shapesList.Count() < 1)
            {
                MessageBox.Show("Add a shape");
                return;
            }

            _mainForm.formFactory.CreateForm("Shapes Info Form");
        }
    }
}
