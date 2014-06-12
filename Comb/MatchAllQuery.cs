namespace Comb
{
    /// <summary>
    /// A special structured query which matches all documents in the index.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/search-api.html#structured-search-syntax
    /// </summary>
    public class MatchAllQuery : Query
    {
        public override string Parser
        {
            get { return "structured"; }
        }

        public override string Definition
        {
            get { return "matchall"; }
        }
    }
}