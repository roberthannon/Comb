namespace Comb.Searching.Queries
{
    /// <summary>
    /// Defines how results are found using the 'simple' query syntax.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/search-api.html#simple-search-syntax
    /// </summary>
    public class SimpleQuery : Query
    {
        readonly string _definition;

        public SimpleQuery(string definition)
        {
            _definition = definition;
        }

        public override string Parser
        {
            get { return "simple"; }
        }

        public override string Definition { get { return _definition; } }
    }
}