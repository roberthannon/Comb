using Comb.Searching.Queries.Structured;

namespace Comb.Searching.Queries
{
    /// <summary>
    /// Defines how results are found using the 'structured' query syntax.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/search-api.html#structured-search-syntax
    /// </summary>
    public class StructuredQuery : Query
    {
        readonly Condition _root;

        public StructuredQuery(Condition root)
        {
            _root = root;
        }

        public override string Parser
        {
            get { return "structured"; }
        }

        public override string Definition
        {
            get { return _root.Definition; }
        }
    }
}