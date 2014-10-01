namespace Comb.StructuredQueries
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
            get { return Value.ToString(); }
        }
    }
}