using System.Globalization;

namespace Comb
{
    public class IntValue : IOperand
    {
        public IntValue(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public string Definition => ToString();

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}