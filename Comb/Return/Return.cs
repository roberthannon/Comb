using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comb
{
    public class Return
    {
        public static readonly Return AllFields = new Return(ReturnFields.All);
        public static readonly Return Score = new Return(ReturnFields.Score);

        public IField Field { get; private set; }

        public Return(IField field)
        {
            Field = field;
        }

        public Return(string fieldName)
        {
            Field = new Field(fieldName);
        }

        public override string ToString()
        {
            return Field.Name;
        }
    }
}
