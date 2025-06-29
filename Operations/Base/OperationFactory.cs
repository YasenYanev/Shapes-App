using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Shapes.Base;
using Coursework.Shapes.Types;
using Coursework.Interfaces;

namespace Coursework.Operations.Base
{
    public class OperationFactory
    {
        private readonly List<Operation> _operationsFactory = new List<Operation>();

        public void RegisterOperation(Operation operation)
        {
            _operationsFactory.Add(operation);
        }

        public Operation? GetOperationByName(string name)
        {
            return _operationsFactory.FirstOrDefault(op => op.Name == name);
        }
    }
}