namespace Comb.StructuredQueries
{
    public class PrefixCondition : UniOperator
    {
        public PrefixCondition(StringValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public PrefixCondition(string operand, string field, uint? boost = null)
            : this(new StringValue(operand), new Field(field), boost)
        {
        }

        public override string Opcode { get { return "prefix"; } }
    }
}