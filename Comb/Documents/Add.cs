using Newtonsoft.Json;

namespace Comb.Documents
{
    public class Add : DocumentRequest
    {
        [JsonProperty("fields")]
        public object Fields { get; private set; }

        public Add(string id, object document)
            : base("add", id)
        {
            Fields = document;
        }
    }
}