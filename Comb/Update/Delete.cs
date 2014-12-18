namespace Comb
{
    public class Delete : DocumentRequest
    {
        public Delete(string id)
            : base("delete", id)
        {
        }
    }
}