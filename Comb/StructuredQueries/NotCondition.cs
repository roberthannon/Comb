using System;
using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public class NotCondition : UniOperator
    {
        public NotCondition(IOperand operand, uint? boost = null, string field = null)
            : base("not", operand)
        {
            Field = field;
            Boost = boost;
        }
    }
}