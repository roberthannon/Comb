using System;

namespace Comb
{
    /// <summary>
    /// Defines how results are found using the 'structured' query syntax.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/search-api.html#structured-search-syntax
    /// </summary>
    public class StructuredQuery : Query
    {
        public StructuredQuery(IOperator root)
        {
            if (root == null) throw new ArgumentNullException(nameof(root));

            Root = root;
        }

        public IOperator Root { get; }

        public override string Parser => "structured";

        public override string Definition => Root.Definition;
    }
}