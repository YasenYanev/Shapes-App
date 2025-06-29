using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coursework.Operations.Base;
using Coursework.Shapes.Base;

namespace Coursework.Operations.Types
{
    internal class SelectOperation : Operation
    {
        private MainForm _mainForm;
        public delegate void TryShapeSelectD(Shape shape);
        public event TryShapeSelectD TryShapeSelect;
        public override string Name => "Select";

        public SelectOperation(MainForm mainForm)
        {
            _mainForm = mainForm;
            TryShapeSelect += OnTryShapeSelect;
        }
        public override void Execute(params object[] args)
        {
            TryShapeSelect?.Invoke((Shape)args[0]);
        }
        private void OnTryShapeSelect(Shape? shape)
        {
            if (shape != null && shape == _mainForm.selectedShape) return;
            if (_mainForm.selectedShape != null) _mainForm.selectedShape.IsSelected = false;
            if (shape == null)
            {
                _mainForm.selectedShape = null;
                _mainForm.panelProperties.Controls.Clear();
                _mainForm.panelCanvas.Refresh();
                return;
            }

            _mainForm.selectedShape = shape;
            _mainForm.selectedShape.IsSelected = true;

            _mainForm.formFactory.CreateForm("Properties Form");
            _mainForm.panelCanvas.Refresh();
        }
    }
}
