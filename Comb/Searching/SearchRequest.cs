using System.Collections.Generic;
using Comb.Searching.Queries;

namespace Comb.Searching
{
    public class SearchRequest
    {
        public uint? Start { get; set; }
        public uint? Size { get; set; }

        public List<Sort> Sort { get; set; }

        public Query Query { get; set; }

        public SearchRequest()
        {
            Sort = new List<Sort>();
        }
    }
}