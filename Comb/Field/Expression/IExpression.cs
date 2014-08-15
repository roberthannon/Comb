using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comb
{
    public interface IExpression : IField
    {
        string Definition { get; }
    }
}
