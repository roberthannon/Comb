using System.Collections.Generic;
using System.Data.Common;

namespace Comb.Queries
{
    public class SearchQuery
    {
        public uint? Start { get; set; }
        public uint? Size { get; set; }

        public List<Sort> Sort { get; set; }

        public SearchQuery()
        {
            Sort = new List<Sort>();
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class Sort
    {
        public static readonly Sort Id = new Sort();

        public string Name { get; private set; }
        public SortDirection Direction { get; private set; }

        public Sort(string name, SortDirection direction)
        {
            Name = name;
            Direction = direction;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Direction == SortDirection.Ascending ? "asc" : "desc");
        }
    }

    public class Expression
    {
        public string Name { get; set; }
        public string Definition { get; set; }
    }
}
