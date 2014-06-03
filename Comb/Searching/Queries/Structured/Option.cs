namespace Comb.Searching.Queries.Structured
{
    public class Option
    {
        public Option(string name, string value)
        {
            Value = value;
            Name = name;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }
    }
}