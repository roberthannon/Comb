using System;
using System.Net;

namespace Comb
{
    /// <summary>
    /// An error occured while making a search request. Refer to the following API best practices on
    /// on handling and retrying these errors.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/error-handling.html
    /// http://docs.aws.amazon.com/general/latest/gr/api-retries.html
    /// </summary>
    public class SearchException : Exception
    {
        /// <summary>
        /// Information about the request that caused the exception.
        /// </summary>
        public SearchInfo Request { get; protected set; }

        /// <summary>
        /// The HTTP status code returned by CloudSearch.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; protected set; }

        /// <summary>
        /// Returns true if this is a transient CloudSearch error that Amazon recommends you retry,
        /// otherwise false.
        /// </summary>
        public bool ShouldRetry
        {
            get { return 500 <= (int) HttpStatusCode; }
        }

        public SearchException(SearchInfo request, HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            Request = request;
            HttpStatusCode = httpStatusCode;
        }
    }
}
