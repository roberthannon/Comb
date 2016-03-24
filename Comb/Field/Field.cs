using System;
using System.Text.RegularExpressions;

namespace Comb
{
    public class Field : IField
    {
        public Field(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!Regex.IsMatch(name, Constants.FieldNameFormat))
                throw new ArgumentException($"Invalid field name: {name}", nameof(name));

            if (Constants.ReservedFieldNames.Contains(name))
                throw new ArgumentException($"Reserved field name: {name}", nameof(name));

            Name = name;
        }

        public string Name { get; }

        // TODO Might be useful
        //public FieldType Type { get; }
    }
}
