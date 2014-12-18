using System.Globalization;

namespace Comb
{
    public class DoubleValue : IOperand
    {
        readonly double _value;

        public DoubleValue(double value)
        {
            _value = value;
        }

        public double Value { get { return _value; } }

        public string Definition
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
}