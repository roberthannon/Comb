using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public class OrCondition : Operator
    {
        public OrCondition(ICollection<IOperand> operands, uint? boost = null, string field = null)
            : base("or", operands)
        {
            Field = field;
            Boost = boost;
        }
    }
}