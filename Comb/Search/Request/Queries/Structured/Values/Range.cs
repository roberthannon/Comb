using System;

namespace Comb
{
    public class Range : IOperand
    {
        readonly IOperand _min;
        readonly IOperand _max;
        readonly bool _minInclusive;
        readonly bool _maxInclusive;

        private Range(IOperand min = null, IOperand max = null, bool minInclusive = false, bool maxInclusive = false)
        {
            if (min == null && max == null)
                throw new ArgumentException("Min and max cannot both be null.");

            // TODO check min < max?

            _min = min;
            _max = max;
            _minInclusive = minInclusive;
            _maxInclusive = maxInclusive;
        }

        public Range(IntValue min = null, IntValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IOperand)min, max, minInclusive, maxInclusive)
        {
        }

        public Range(DoubleValue min = null, DoubleValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IOperand)min, max, minInclusive, maxInclusive)
        {
        }

        public Range(DateValue min = null, DateValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IOperand)min, max, minInclusive, maxInclusive)
        {
        }

        public Range(StringValue min = null, StringValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IOperand)min, max, minInclusive, maxInclusive)
        {
        }

        /// <summary>
        /// Bounding box search.
        /// </summary>
        public Range(LatLon min = null, LatLon max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IOperand)min, max, minInclusive, maxInclusive)
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

        public Range(string min = null, string max = null, bool minInclusive = false, bool maxInclusive = false)
            : this(min != null ? new StringValue(min) : null, max != null ? new StringValue(max) : null, minInclusive, maxInclusive)
        {
        }

        public IOperand Min { get { return _min; } }
        public IOperand Max { get { return _max; } }

        public string Definition
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return string.Format("{2}{0},{1}{3}",
                _min != null ? _min.Definition : "",
                _max != null ? _max.Definition : "",
                _minInclusive ? "[" : "{",
                _maxInclusive ? "]" : "}");
        }
    }
}