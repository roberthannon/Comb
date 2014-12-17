namespace Comb
{
    /// <summary>
    /// Defines how results are found using the 'structured' query syntax.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/search-api.html#structured-search-syntax
    /// </summary>
    public class StructuredQuery : Query
    {
        readonly IOperator _root;

        public StructuredQuery(IOperator root)
        {
            _root = root;
        }

        public IOperator Root { get { return _root; } }

        public override string Parser
        {
            get { return "structured"; }
        }

        public override string Definition
        {
            get { return Root.Definition; }
        }
    }
}