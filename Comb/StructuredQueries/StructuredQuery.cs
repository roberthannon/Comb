namespace Comb.StructuredQueries
{
    /// <summary>
    /// Defines how results are found using the 'structured' query syntax.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/search-api.html#structured-search-syntax
    /// </summary>
    public class StructuredQuery : Query
    {
        public StructuredQuery(IOperand root)
        {
            Root = root;
        }

        public IOperand Root { get; private set; }

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