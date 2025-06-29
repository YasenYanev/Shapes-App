using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework.Interfaces;
using Coursework.Shapes.Base;

namespace Coursework.Operations.Base
{
    public abstract class Operation : IOperation
    {
        public abstract string Name { get; }
        public abstract void Execute(params object[] args);
    }
}
