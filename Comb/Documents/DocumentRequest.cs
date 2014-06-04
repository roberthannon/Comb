namespace Comb.Documents
{
    public abstract class DocumentRequest
    {
        public string Type { get; private set; }

        public string Id { get; private set; }

        protected DocumentRequest(string type, string id)
        {
            Type = type;
            Id = id;
        }
    }
}
