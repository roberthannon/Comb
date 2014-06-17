using System.Net;
using System.Net.Http;

namespace Comb.Tests.Support
{
    public static class ResponseSamples
    {
        public static HttpResponseMessage OK()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new TestHttpContent(@"{
                    ""status"":{
                        ""rid"":""/aK08egovR4K+x+p"",
                        ""time-ms"":1
                    },
                    ""hits"":{
                        ""found"":0,
                        ""start"":0,
                        ""hit"":[]
                    }
                }")
            };
        }

        public static HttpResponseMessage BadRequest(string message = "You did something NASTY.")
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new TestHttpContent(@"{
	                ""error"" : {
		                ""rid"" : ""iPXcuuooRwr7hS0=""
	                },
	                ""message"" : """ + message + @""",
	                ""__type"" : ""#SearchException""
                }")
            };
        }
    }
}
