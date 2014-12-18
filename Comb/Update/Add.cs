using Newtonsoft.Json;

namespace Comb
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