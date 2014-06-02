namespace Comb.Searching.Queries
{
    /// <summary>
    /// Defines how results are found using the 'simple' query syntax.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/searching-text.html
    /// </summary>
    public class SimpleQuery : Query
    {
        public SimpleQuery(string definition)
        {
            Definition = definition;
        }

        public string Definition { get; private set; }
    }
}