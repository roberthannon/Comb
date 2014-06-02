namespace Comb.Searching.Responses
{
    public interface ISearchResponse
    {
        /// <summary>
        /// The unique identifier for the document found.
        /// </summary>
        string Id { get; set; }
    }
}