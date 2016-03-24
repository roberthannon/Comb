using System;

namespace Comb
{
    public class Range : IOperand
    {
        private Range(IOperand min = null, IOperand max = null, bool minInclusive = false, bool maxInclusive = false)
        {
            if (min == null && max == null)
                throw new ArgumentException("Min and max cannot both be null.");

            // TODO check min < max?

            Min = min;
            Max = max;
            MinInclusive = minInclusive;
            MaxInclusive = maxInclusive;
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
            //if (min == null || max == null) // TODO?
            //    throw new ArgumentException("Range query on a latlonfield cannot be open ended.");
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

        public IOperand Min { get; }
        public IOperand Max { get; }
        public bool MinInclusive { get; }
        public bool MaxInclusive { get; }

        public string Definition => ToString();

        public override string ToString()
        {
            var min = Min != null ? Min.Definition : "";
            var max = Max != null ? Max.Definition : "";
            var open = MinInclusive ? "[" : "{";
            var close = MaxInclusive ? "]" : "}";

            return $"{open}{min},{max}{close}";
        }
    }
}