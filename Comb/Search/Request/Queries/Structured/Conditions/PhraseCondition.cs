namespace Comb
{
    public class PhraseCondition : UniOperator
    {
        public PhraseCondition(StringValue operand, IField field = null, uint? boost = null)
            : base(operand, field, boost)
        {
        }

        public PhraseCondition(string operand, string field = null, uint? boost = null)
            : this(new StringValue(operand), field != null ? new Field(field) : null, boost)
        {
        }

        public override string Opcode { get { return "phrase"; } }
    }
}