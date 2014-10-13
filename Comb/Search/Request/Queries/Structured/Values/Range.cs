using System;

namespace Comb
{
    public class Range : IOperand
    {
        readonly IValue _min;
        readonly IValue _max;
        readonly bool _minInclusive;
        readonly bool _maxInclusive;

        private Range(IValue min = null, IValue max = null, bool minInclusive = false, bool maxInclusive = false)
        {
            if (min == null && max == null)
                throw new ArgumentNullException("min", "Both min and max cannot be null.");

            // TODO check min < max?

            _min = min;
            _max = max;
            _minInclusive = minInclusive;
            _maxInclusive = maxInclusive;
        }

        public Range(IntValue min = null, IntValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IValue)min, max, minInclusive, maxInclusive)
        {
        }

        public Range(DoubleValue min = null, DoubleValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IValue)min, max, minInclusive, maxInclusive)
        {
        }

        public Range(DateValue min = null, DateValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IValue)min, max, minInclusive, maxInclusive)
        {
        }

        public Range(int? min = null, int? max = null, bool minInclusive = false, bool maxInclusive = false)
            : this(min != null ? new IntValue(min.Value) : null, max != null ? new IntValue(max.Value) : null, minInclusive, maxInclusive)
        {
        }

        public Range(double? min = null, double? max = null, bool minInclusive = false, bool maxInclusive = false)
            : this(min != null ? new DoubleValue(min.Value) : null, max != null ? new DoubleValue(max.Value) : null, minInclusive, maxInclusive)
        {
        }

        public Range(DateTime? min = null, DateTime? max = null, bool minInclusive = false, bool maxInclusive = false)
            : this(min != null ? new DateValue(min.Value) : null, max != null ? new DateValue(max.Value) : null, minInclusive, maxInclusive)
        {
        }

        public IValue Min { get { return _min; } }

        public IValue Max { get { return _max; } }

        public string QueryDefinition
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return string.Format("{2}{0},{1}{3}",
                _min != null ? _min.QueryDefinition : "",
                _max != null ? _max.QueryDefinition : "",
                _minInclusive ? "[" : "{",
                _maxInclusive ? "]" : "}");
        }
    }
}