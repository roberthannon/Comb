using System;

namespace Comb
{
    public class RangeCondition : UniOperator
    {
        public RangeCondition(Range operand, IField field, uint? boost = null)
            : base(operand, field, boost)
        {
            if (field == null)
                throw new ArgumentNullException("field");
        }

        public RangeCondition(Range operand, string field, uint? boost = null)
            : this(operand, new Field(field), boost)
        {
        }

        public override string Opcode { get { return "range"; } }
    }
}