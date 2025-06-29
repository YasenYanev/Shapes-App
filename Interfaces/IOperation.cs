using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Interfaces
{
    public interface IOperation
    {
        public string Name { get; }
        public void Execute(params object[] args);
    }
}
