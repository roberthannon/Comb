namespace Comb
{
    public class NotCondition : UniOperator
    {
        public NotCondition(IOperator operand, uint? boost = null)
            : base(operand, null, boost)
        {
        }

        public override string Opcode => "not";
    }
}