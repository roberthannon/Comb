namespace Comb.StructuredQueries
{
    /// <summary>
    /// Defines how results are found using the 'structured' query syntax.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/search-api.html#structured-search-syntax
    /// </summary>
    public class StructuredQuery : Query
    {
        readonly IOperand _root;

        public StructuredQuery(IOperand root)
        {
            _root = root;
        }

        public IOperand Root { get { return _root; } }

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