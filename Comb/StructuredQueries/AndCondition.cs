using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public class AndCondition : Operator
    {
        public AndCondition(ICollection<IOperand> operands, uint? boost = null, string field = null)
            : base("and", operands)
        {
            Field = field;
            Boost = boost;
        }
    }
}