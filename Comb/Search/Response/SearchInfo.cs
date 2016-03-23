using System;
using System.Collections.Generic;

namespace Comb
{
    /// <summary>
    /// Information recorded by the CloudSearch client as it builds the request.
    /// </summary>
    public class SearchInfo
    {
        public string Url { get; }

        public IDictionary<string, string> Parameters { get; }

        public SearchInfo(string url, IDictionary<string, string> parameters)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Url = url;
            Parameters = parameters;
        }
    }
}