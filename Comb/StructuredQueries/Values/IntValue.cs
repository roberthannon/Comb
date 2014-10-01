namespace Comb.StructuredQueries
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
            get { return Value.ToString(); }
        }
    }
}