namespace Comb
{
    public class Hit<T>
    {
        /// <summary>
        /// The unique identifier for the document found.
        /// </summary>
        public string Id { get; set; }

        public T Fields { get; set; }
    }
}