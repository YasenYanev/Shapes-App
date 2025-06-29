using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Shapes.Base;

namespace Coursework.Utilities
{
    public class FormFactory
    {
        private MainForm _mainForm;
        private readonly Dictionary<string, Action> _formsFactory
            = new Dictionary<string, Action>();

        public void RegisterForm(string formName, Action func)
        {
            _formsFactory[formName] = func;
        }
        public void CreateForm(string formName)
        {
            if (_formsFactory.ContainsKey(formName)) _formsFactory[formName]();
            else throw new Exception($"{formName} does not exist");
        }
    }
}
