namespace Comb.StructuredQueries
{
    public class NotCondition : UniOperator
    {
        public NotCondition(IOperand operand, uint? boost = null)
            : base(operand, null, boost)
        {
        }

        public override string Opcode { get { return "not"; } }
    }
}