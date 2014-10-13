namespace Comb
{
    public class IntValue : IValue
    {
        readonly int _value;

        public IntValue(int value)
        {
            _value = value;
        }

        public int Value { get { return _value; } }

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