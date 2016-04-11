using System;

namespace Comb
{
    public class Return
    {
        public Return(IField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            Field = field;
        }

        public Return(string fieldName)
            : this(new Field(fieldName))
        {
        }

        public IField Field { get; }

        public override string ToString()
        {
            return Field.Name;
        }
    }
}
