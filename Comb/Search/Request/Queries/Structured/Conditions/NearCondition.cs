namespace Comb
{
    public class NearCondition : UniOperator
    {
        public NearCondition(StringValue operand, IField field = null, uint? boost = null, uint? distance = null)
            : base(operand, field, boost)
        {
            Distance = distance;

            if (Distance.HasValue) AddOption("distance", Distance.ToString());
        }

        public NearCondition(string operand, string field = null, uint? boost = null, uint? distance = null)
            : this(new StringValue(operand), field != null ? new Field(field): null, boost, distance)
        {
        }

        public override string Opcode => "near";

        public uint? Distance { get; }
    }
}