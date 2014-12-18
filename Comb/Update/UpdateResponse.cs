namespace Comb
{
    public class UpdateResponse
    {
        public string Status { get; set; } // TODO make enum?

        public int Adds { get; set; }

        public int Deletes { get; set; }

        public string Message { get; set; }

        public UpdateResponseMessage[] Errors { get; set; }

        public UpdateResponseMessage[] Warnings { get; set; }

        public class UpdateResponseMessage
        {
            public string Message { get; set; }
        }
    }
}
