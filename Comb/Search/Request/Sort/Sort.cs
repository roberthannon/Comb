using System;

namespace Comb
{
    public class Sort
    {
        public Sort(IField field, SortDirection direction)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field));

            Field = field;
            Direction = direction;
        }

        public Sort(string fieldName, SortDirection direction)
            : this(new Field(fieldName), direction)
        {
        }

        public IField Field { get; }

        public SortDirection Direction { get; }

        public override string ToString()
        {
            return $"{Field.Name} {(Direction == SortDirection.Ascending ? "asc" : "desc")}";
        }
    }
}