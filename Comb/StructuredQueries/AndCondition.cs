using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public class AndCondition : Operator
    {
        public AndCondition(IEnumerable<IOperand> operands, uint? boost = null, string field = null)
            : base("and", operands)
        {
            Field = field;
            Boost = boost;
        }
    }
}