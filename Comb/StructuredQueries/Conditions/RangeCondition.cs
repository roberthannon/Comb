using System;

namespace Comb.StructuredQueries
{
    public class RangeCondition : UniOperator
    {
        public RangeCondition(RangeValue operand, IField field, uint? boost = null)
            : base(operand, field, boost)
        {
            if (field == null)
                throw new ArgumentNullException("field");
        }

        public RangeCondition(RangeValue operand, string field, uint? boost = null)
            : this(operand, new Field(field), boost)
        {
        }

        public override string Opcode { get { return "range"; } }
    }
}