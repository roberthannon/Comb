using System.Net;

namespace Comb
{
    /// <summary>
    /// An error occured while making an update request. Refer to the following API best practices on
    /// on handling and retrying these errors.
    /// http://docs.aws.amazon.com/cloudsearch/latest/developerguide/error-handling.html
    /// http://docs.aws.amazon.com/general/latest/gr/api-retries.html
    /// </summary>
    public class UpdateException : CloudSearchException
    {
        /// <summary>
        /// The response object returned from CloudSearch.
        /// </summary>
        public UpdateResponse Response { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateException" /> with the given response,
        /// status code, and message.
        /// </summary>
        public UpdateException(UpdateResponse response, HttpStatusCode httpStatusCode, string message)
            : base(httpStatusCode, message)
        {
            Response = response;
        }
    }
}
