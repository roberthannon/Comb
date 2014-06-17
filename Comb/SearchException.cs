using System;
using System.Net;

namespace Comb
{
    public class SearchException : Exception
    {
        public SearchInfo Request { get; protected set; }

        public HttpStatusCode HttpStatusCode { get; protected set; }

        public SearchException(SearchInfo request, HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            Request = request;
            HttpStatusCode = httpStatusCode;
        }
    }
}
