using Newtonsoft.Json;

namespace Comb.Responses
{
    public class Status
    {
        /// <summary>
        /// The encrypted Resource ID.
        /// </summary>
        [JsonProperty("rid")]
        public string ResourceId { get; set; }

        /// <summary>
        /// How long it took to process the search request in milliseconds.
        /// </summary>
        [JsonProperty("time-ms")]
        public int TimeMs { get; set; }
    }
}