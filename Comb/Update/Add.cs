namespace Comb
{
    public class Add : DocumentRequest
    {
        public object Fields { get; }

        public Add(string id, object document)
            : base(UpdateType.Add, id)
        {
            Fields = document;
        }
    }
}