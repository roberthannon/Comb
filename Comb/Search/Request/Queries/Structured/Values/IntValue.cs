using System.Globalization;

namespace Comb
{
    public class IntValue : IOperand
    {
        readonly int _value;

        public IntValue(int value)
        {
            _value = value;
        }

        public int Value { get { return _value; } }

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