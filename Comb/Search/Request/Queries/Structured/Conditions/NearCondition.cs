namespace Comb
{
    public class NearCondition : UniOperator
    {
        readonly uint? _distance;

        public NearCondition(StringValue operand, IField field = null, uint? boost = null, uint? distance = null)
            : base(operand, field, boost)
        {
            _distance = distance;

            if (Distance.HasValue) AddOption("distance", Distance.ToString());
        }

        public NearCondition(string operand, string field = null, uint? boost = null, uint? distance = null)
            : this(new StringValue(operand), field != null ? new Field(field): null, boost, distance)
        {
        }

        public override string Opcode { get { return "near"; } }

        public uint? Distance { get { return _distance; } }
    }
}