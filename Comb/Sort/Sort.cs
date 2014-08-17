namespace Comb
{
    public class Sort
    {
        public IField Field { get; private set; }
        public SortDirection Direction { get; private set; }

        public Sort(IField field, SortDirection direction)
        {
            Field = field;
            Direction = direction;
        }

        public Sort(string fieldName, SortDirection direction)
        {
            Field = new Field(fieldName);
            Direction = direction;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Field.Name, Direction == SortDirection.Ascending ? "asc" : "desc");
        }
    }
}