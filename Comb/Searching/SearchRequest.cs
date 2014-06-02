using System.Collections.Generic;

namespace Comb.Queries
{
    public class SearchRequest
    {
        public uint? Start { get; set; }
        public uint? Size { get; set; }

        public List<Sort> Sort { get; set; }

        public SearchRequest()
        {
            Sort = new List<Sort>();
        }
    }
}