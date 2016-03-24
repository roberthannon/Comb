namespace Comb
{
    public class PrefixCondition : UniOperator
    {
        public PrefixCondition(StringValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public PrefixCondition(string operand, string field = null, uint? boost = null)
            : this(new StringValue(operand), field != null ? new Field(field) : null, boost)
        {
        }

        public override string Opcode => "prefix";
    }
}