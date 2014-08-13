using System.Collections.Generic;

namespace Comb
{
    public class SearchRequest
    {
        public uint? Start { get; set; }
        public uint? Size { get; set; }

        public List<Sort> Sort { get; set; }

        public Query Query { get; set; }

        public List<string> Return { get; set; }

        public SearchRequest()
        {
            Sort = new List<Sort>();
            Return = new List<string>();
        }
    }
}