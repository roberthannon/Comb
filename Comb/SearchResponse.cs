namespace Comb
{
    public class SearchResponse<T>
    {
        public SearchInfo Info { get; set; }

        public Status Status { get; set; }

        /// <summary>
        /// Contains the number of matching documents, the index of the first document included in
        /// the response, and an array that lists the document IDs and data for each hit.
        /// </summary>
        public Hits<T> Hits { get; set; }

        public SearchResponse()
        {
            Status = new Status();
            Hits = new Hits<T>();
        }
    }
}