using Newtonsoft.Json;

namespace Comb
{
    public abstract class DocumentRequest
    {
        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("id")]
        public string Id { get; private set; }

        protected DocumentRequest(string type, string id)
        {
            Type = type;
            Id = id;
        }
    }
}
