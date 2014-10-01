using System;

namespace Comb
{
    /// <summary>
    /// Custom expression for this query.
    /// </summary>
    public class Expression : IExpression
    {
        readonly string _name;
        readonly string _definition;

        public Expression(string name, string definition)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (definition == null)
                throw new ArgumentNullException("definition");

            _name = name;
            _definition = definition;
        }

        public string Name { get { return _name; } }

        public string Definition { get { return _definition; } }
    }
}