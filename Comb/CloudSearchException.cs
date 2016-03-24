using System;
using System.Net;

namespace Comb
{
    public abstract class CloudSearchException : Exception
    {
        /// <summary>
        /// The HTTP status code returned by CloudSearch.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Returns true if this is a transient CloudSearch error that Amazon recommends you retry,
        /// otherwise false.
        /// </summary>
        public bool ShouldRetry => 500 <= (int)HttpStatusCode;

        /// <summary>
        /// Initializes a new instance of <see cref="CloudSearchException" /> with the given status
        /// code, and message.
        /// </summary>
        protected CloudSearchException(HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
