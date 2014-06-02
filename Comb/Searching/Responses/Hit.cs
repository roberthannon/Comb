namespace Comb.Searching.Responses
{
    public class Hit<T>
        where T : ISearchResponse
    {
        /// <summary>
        /// The unique identifier for the document found.
        /// </summary>
        public string Id { get; set; }

        public T Fields { get; set; }
    }
}