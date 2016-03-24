using System;

namespace Comb
{
    /// <summary>
    /// Defines how results are found using the 'simple' query syntax.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/search-api.html#simple-search-syntax
    /// </summary>
    public class SimpleQuery : Query
    {
        public SimpleQuery(string definition)
        {
            if (definition == null) throw new ArgumentNullException(nameof(definition));

            Definition = definition;
        }

        public override string Parser => "simple";

        public override string Definition { get; }
    }
}