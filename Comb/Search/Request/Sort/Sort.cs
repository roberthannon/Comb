using System;

namespace Comb
{
    public class Sort
    {
        readonly IField _field;
        readonly SortDirection _direction;

        public Sort(IField field, SortDirection direction)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            _field = field;
            _direction = direction;
        }

        public Sort(string fieldName, SortDirection direction)
            : this(new Field(fieldName), direction)
        {
        }

        public IField Field { get { return _field; } }

        public SortDirection Direction { get { return _direction; } }

        public override string ToString()
        {
            return string.Format("{0} {1}", Field.Name, Direction == SortDirection.Ascending ? "asc" : "desc");
        }
    }
}