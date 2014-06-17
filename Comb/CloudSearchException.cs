using System;
using System.Net;

namespace Comb
{
    public class CloudSearchException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; protected set; }

        public CloudSearchException(HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
