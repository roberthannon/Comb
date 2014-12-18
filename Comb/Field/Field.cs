using System;
using System.Text.RegularExpressions;

namespace Comb
{
    public class Field : IField
    {
        readonly string _name;

        public Field(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (!Regex.IsMatch(name, Constants.FieldNameFormat))
                throw new ArgumentException(string.Format("Invalid field name: {0}", name), "name");

            if (Constants.ReservedFieldNames.Contains(name))
                throw new ArgumentException(string.Format("Reserved field name: {0}", name), "name");

            _name = name;
        }

        public string Name { get { return _name; } }

        // TODO Might be useful
        //public FieldType Type { get; set; }
    }
}
