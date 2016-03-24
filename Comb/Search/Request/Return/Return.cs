using System;

namespace Comb
{
    public class Return
    {
        public static readonly Return AllFields = new Return(ReturnFields.AllFields);
        public static readonly Return NoFields = new Return(ReturnFields.NoFields);
        public static readonly Return Score = new Return(ReturnFields.Score);

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
