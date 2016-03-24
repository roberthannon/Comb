using System.Globalization;

namespace Comb
{
    public class DoubleValue : IOperand
    {
        public DoubleValue(double value)
        {
            Value = value;
        }

        public double Value { get; }

        public string Definition => ToString();

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}