using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public class NearCondition : UniOperator
    {
        readonly uint? _distance;

        public NearCondition(StringValue operand, IField field = null, uint? boost = null, uint? distance = null)
            : base(operand, field, boost)
        {
            _distance = distance;
        }

        public NearCondition(string operand, string field, uint? boost = null, uint? distance = null)
            : this(new StringValue(operand), new Field(field), boost, distance)
        {
        }

        public override string Opcode { get { return "near"; } }

        public uint? Distance { get { return _distance; } }

        public override IEnumerable<Option> Options
        {
            get
            {
                if (Field != null)
                    yield return new Option("field", Field.Name);
                if (Boost.HasValue)
                    yield return new Option("boost", Boost.ToString());
                if (Distance.HasValue)
                    yield return new Option("distance", Distance.ToString());
            }
        }
    }
}