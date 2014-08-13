namespace Comb
{
    /// <summary>
    /// Information recorded by the CloudSearch client as it builds the request.
    /// </summary>
    public class SearchInfo
    {
        public string Url    { get; set; }
        public string Parser { get; set; }
        public string Query  { get; set; }
        public string Size   { get; set; }
        public string Start  { get; set; }
        public string Sort   { get; set; }
        public string Return { get; set; }
    }
}