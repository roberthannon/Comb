namespace Comb.Documents
{
    public class Add : DocumentRequest
    {
        public Add(string id, object document)
            : base("add", id)
        {
        }
    }
}