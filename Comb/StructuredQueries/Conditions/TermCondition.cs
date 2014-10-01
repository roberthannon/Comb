using System;

namespace Comb.StructuredQueries
{
    public class TermCondition : UniOperator
    {
        public TermCondition(StringValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public TermCondition(IntValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public TermCondition(DoubleValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public TermCondition(DateValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public TermCondition(string operand, string field = null, uint? boost = null)
            : base(new StringValue(operand), new Field(field), boost)
        {
        }

        public TermCondition(int operand, string field = null, uint? boost = null)
            : base(new IntValue(operand), new Field(field), boost)
        {
        }

        public TermCondition(double operand, string field = null, uint? boost = null)
            : base(new DoubleValue(operand), new Field(field), boost)
        {
        }

        public TermCondition(DateTime operand, string field = null, uint? boost = null)
            : base(new DateValue(operand), new Field(field), boost)
        {
        }

        public override string Opcode { get { return "term"; } }
    }
}