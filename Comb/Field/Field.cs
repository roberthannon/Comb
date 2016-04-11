using System;

namespace Comb
{
    public class Field : IField
    {
        public Field(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (!IsValid(name)) throw new ArgumentException($"Invalid field name: {name}", nameof(name));

            Name = name;
        }

        public string Name { get; }

        // TODO Might be useful
        //public FieldType Type { get; }

        public static bool IsValid(string name)
        {
            //return Fields.BuiltIn.Contains(name) || !Fields.Reserved.Contains(name) && Fields.Format.IsMatch(name);
            return true;
        }
    }
}
