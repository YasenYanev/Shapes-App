using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Operations.Base;
using Coursework.Shapes.Base;

namespace Coursework.Operations.Types
{
    public class EditOperation : Operation
    {
        public MainForm _mainForm;
        public override string Name => "Edit";

        public EditOperation(MainForm mainForm) {
            _mainForm = mainForm;
        }

        public override void Execute(params object[] args)
        {
            List<int> props = (List<int>)args[0];
            Color selectedInnerColor = (Color)args[1];
            Color selectedBorderColor = (Color)args[2];

            _mainForm.selectedShape.UpdatePropreties(selectedBorderColor, selectedInnerColor, props);
        }
    }
}
