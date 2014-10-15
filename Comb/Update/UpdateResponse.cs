using System;

namespace Comb
{
    public class UpdateResponse
    {
        public string Status { get; set; }

        public int Adds { get; set; }

        public int Deletes { get; set; }

        public string Message { get; set; }

        [Obsolete("Deprecated by AWS")]
        public UpdateResponseMessage[] Errors { get; set; }

        [Obsolete("Deprecated by AWS")]
        public UpdateResponseMessage[] Warnings { get; set; }

        public class UpdateResponseMessage
        {
            public string Message { get; set; }
        }
    }
}
