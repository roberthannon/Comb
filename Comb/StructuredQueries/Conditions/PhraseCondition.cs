namespace Comb.StructuredQueries
{
    public class PhraseCondition : UniOperator
    {
        public PhraseCondition(StringValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public PhraseCondition(string operand, string field, uint? boost = null)
            : this(new StringValue(operand), new Field(field), boost)
        {
        }

        public override string Opcode { get { return "phrase"; } }
    }
}