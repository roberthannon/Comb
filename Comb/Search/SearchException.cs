using System.Net;

namespace Comb
{
    /// <summary>
    /// An error occured while making a search request. Refer to the following API best practices on
    /// on handling and retrying these errors.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/error-handling.html
    /// http://docs.aws.amazon.com/general/latest/gr/api-retries.html
    /// </summary>
    public class SearchException : CloudSearchException
    {
        /// <summary>
        /// Information about the search request that caused the exception.
        /// </summary>
        public SearchInfo Request { get; protected set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SearchException" /> with the given info, status
        /// code, and message.
        /// </summary>
        public SearchException(SearchInfo request, HttpStatusCode httpStatusCode, string message)
            : base(httpStatusCode, message)
        {
            Request = request;
        }
    }
}
