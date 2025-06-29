using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Operations.Base;
using Coursework.Shapes.Base;

namespace Coursework.Operations.Types
{
    public class DeleteOperation : Operation
    {
        private MainForm _mainForm;
        public override string Name => "Delete";

        public DeleteOperation(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public override void Execute(params object[] args)
        {
            Shape shape = (Shape)args[0];
            _mainForm.panelCanvas.Paint -= shape.OnPaint;
            _mainForm.shapesList.Remove(shape);
        }
    }
}
