namespace Comb
{
    public class DoubleValue : IValue
    {
        readonly double _value;

        public DoubleValue(double value)
        {
            _value = value;
        }

        public double Value { get { return _value; } }

        public string QueryDefinition
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}