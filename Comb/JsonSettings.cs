using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Comb
{
    /// <summary>
    /// Standard variations on JSON serialization settings for different parts of out infrastructure.
    /// </summary>
    internal static class JsonSettings
    {
        public static readonly JsonSerializerSettings Default;

        static JsonSettings()
        {
            var camelCaseEnums = new StringEnumConverter { CamelCaseText = true };

            Default = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                //DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.None,
                Converters = new List<JsonConverter> { camelCaseEnums }
            };
        }
    }
}