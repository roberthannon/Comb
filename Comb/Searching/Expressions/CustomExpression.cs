namespace Comb.Searching.Expressions
{
    /// <summary>
    /// Custom expression for this query.
    /// TODO: If used in sort etc, should be added to the custom expressions automatically.
    /// </summary>
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