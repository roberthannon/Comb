namespace Comb.Searching.Responses
{
    public class Hits<T>
    {
        /// <summary>
        /// The total number of hits that match the search request after Amazon CloudSearch finished
        /// processing the request.
        /// </summary>
        public int Found { get; set; }

        /// <summary>
        /// The index of the first hit returned in this response.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// An array containing a result for each document hit.
        /// </summary>
        public Hit<T>[] Hit { get; set; }
    }
}