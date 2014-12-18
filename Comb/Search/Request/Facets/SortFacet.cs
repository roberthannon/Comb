namespace Comb
{
    public class SortFacet : Facet
    {
        readonly FacetSortType _sort;
        readonly int _size;

        public SortFacet(IField field, FacetSortType sort = FacetSortType.Count, int size = 10)
            : base(field)
        {
            _sort = sort;
            _size = size;
        }

        public SortFacet(string field, FacetSortType sort = FacetSortType.Count, int size = 10)
            : this(new Field(field), sort, size)
        {
        }

        public FacetSortType Sort { get { return _sort; } }

        public int Size { get { return _size; } }

        public override string Definition
        {
            get { return string.Format("{{sort:'{0}',size:{1}}}", _sort.ToString().ToLower(), _size); }
        }
    }
}