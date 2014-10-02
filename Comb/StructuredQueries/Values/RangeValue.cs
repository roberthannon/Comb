using System;

namespace Comb.StructuredQueries
{
    public class RangeValue : IOperand
    {
        readonly IOperand _min;
        readonly IOperand _max;
        readonly bool _minInclusive;
        readonly bool _maxInclusive;

        private RangeValue(IOperand min = null, IOperand max = null, bool minInclusive = false, bool maxInclusive = false)
        {
            if (min == null && max == null)
                throw new ArgumentNullException("min", "Both min and max cannot be null.");

            // TODO check min < max?

            _min = min;
            _max = max;
            _minInclusive = minInclusive;
            _maxInclusive = maxInclusive;
        }

        public RangeValue(IntValue min = null, IntValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IOperand)min, max, minInclusive, maxInclusive)
        {
        }

        public RangeValue(DoubleValue min = null, DoubleValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IOperand)min, max, minInclusive, maxInclusive)
        {
        }

        public RangeValue(DateValue min = null, DateValue max = null, bool minInclusive = false, bool maxInclusive = false)
            : this((IOperand)min, max, minInclusive, maxInclusive)
        {
        }

        public RangeValue(int? min = null, int? max = null, bool minInclusive = false, bool maxInclusive = false)
            : this(min != null ? new IntValue(min.Value) : null, max != null ? new IntValue(max.Value) : null, minInclusive, maxInclusive)
        {
        }

        public RangeValue(double? min = null, double? max = null, bool minInclusive = false, bool maxInclusive = false)
            : this(min != null ? new DoubleValue(min.Value) : null, max != null ? new DoubleValue(max.Value) : null, minInclusive, maxInclusive)
        {
        }

        public RangeValue(DateTime? min = null, DateTime? max = null, bool minInclusive = false, bool maxInclusive = false)
            : this(min != null ? new DateValue(min.Value) : null, max != null ? new DateValue(max.Value) : null, minInclusive, maxInclusive)
        {
        }

        public IOperand Min { get { return _min; } }

        public IOperand Max { get { return _max; } }

        public string Definition
        {
            get
            {
                return string.Format("{2}{0},{1}{3}",
                    _min != null ? _min.Definition : "",
                    _max != null ? _max.Definition : "",
                    _minInclusive ? "[" : "{",
                    _maxInclusive ? "]" : "}");
            }
        }
    }
}