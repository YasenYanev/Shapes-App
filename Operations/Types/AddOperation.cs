using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Operations.Base;
using Coursework.Shapes.Base;

namespace Coursework.Operations.Types
{
    public class AddOperation : Operation
    {
        public override string Name => "Add";
        private MainForm _mainForm;
        public AddOperation(MainForm mainForm) { 
            _mainForm = mainForm; 
        }
        public override void Execute(params object[] args) {
            string shapeType = (string)args[0];
            List<int> props = (List<int>)args[1];
            Color innerColor = (Color)args[2];
            Color borderColor = (Color)args[3];

            Shape shape = _mainForm.shapeFactory.CreateShape(shapeType, props, innerColor, borderColor);

            _mainForm.panelCanvas.Paint += shape.OnPaint;
            _mainForm.shapesList.Add(shape);

            _mainForm.operationFactory.GetOperationByName("Select").Execute(shape);
            _mainForm.panelCanvas.Refresh();
        }
    }
}
