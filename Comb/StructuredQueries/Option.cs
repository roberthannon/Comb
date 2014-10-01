namespace Comb.StructuredQueries
{
    public class Option
    {
        readonly string _name, _value;

        public Option(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name { get { return _name; } }

        public string Value { get { return _value; } }
    }
}