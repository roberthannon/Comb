using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public class OrCondition : Operator
    {
        public OrCondition(IEnumerable<IOperand> operands, uint? boost = null, string field = null)
            : base("or", operands)
        {
            Field = field;
            Boost = boost;
        }
    }
}