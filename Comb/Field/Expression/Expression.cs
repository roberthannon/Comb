using System;

namespace Comb
{
    /// <summary>
    /// Custom expression for this query. Used for things like sorting results.
    /// </summary>
    public class Expression : IExpression
    {
        public Expression(string name, string definition)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (definition == null) throw new ArgumentNullException(nameof(definition));

            Name = name;
            Definition = definition;
        }

        public string Name { get; }

        public string Definition { get; }
    }
}