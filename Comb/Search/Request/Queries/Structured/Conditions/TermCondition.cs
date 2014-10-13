using System;

namespace Comb
{
    public class TermCondition : UniOperator
    {
        public TermCondition(StringValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public TermCondition(IntValue operand, IField field, uint? boost = null)
            : base(operand, field, boost)
        {
            if (field == null)
                throw new ArgumentNullException("field"); // Must specify field for non-string values
        }

        public TermCondition(DoubleValue operand, IField field, uint? boost = null)
            : base(operand, field, boost)
        {
            if (field == null)
                throw new ArgumentNullException("field"); // Must specify field for non-string values
        }

        public TermCondition(DateValue operand, IField field, uint? boost = null)
            : base(operand, field, boost)
        {
            if (field == null)
                throw new ArgumentNullException("field"); // Must specify field for non-string values
        }

        public TermCondition(string operand, string field = null, uint? boost = null)
            : this(new StringValue(operand), field != null ? new Field(field) : null, boost)
        {
        }

        public TermCondition(int operand, string field, uint? boost = null)
            : this(new IntValue(operand), new Field(field), boost)
        {
        }

        public TermCondition(double operand, string field, uint? boost = null)
            : this(new DoubleValue(operand), new Field(field), boost)
        {
        }

        public TermCondition(DateTime operand, string field, uint? boost = null)
            : this(new DateValue(operand), new Field(field), boost)
        {
        }

        public override string Opcode { get { return "term"; } }
    }
}