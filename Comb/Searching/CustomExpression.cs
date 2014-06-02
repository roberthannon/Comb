namespace Comb.Searching
{
    public class CustomExpression : Expression
    {
        readonly string _name;
        readonly string _definition;

        public CustomExpression(string name, string definition)
        {
            _name = name;
            _definition = definition;
        }

        public override string Name { get { return _name; } }

        public string Definition { get { return _definition; } }
    }
}