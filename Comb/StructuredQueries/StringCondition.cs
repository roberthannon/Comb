using System;
using System.Text.RegularExpressions;

namespace Comb.StructuredQueries
{
    public class StringCondition : Condition
    {
        readonly string _field;
        readonly string _value;

        public StringCondition(string field, string value)
        {
            if (!string.IsNullOrWhiteSpace(field))
            {
                if (!Regex.IsMatch(field, Constants.FieldNameFormat))
                    throw new ArgumentException(string.Format("Invalid field name: {0}", field), "field");

                if (Constants.ReservedFieldNames.Contains(field))
                    throw new ArgumentException(string.Format("Reserved field name: {0}", field), "field");

                _field = field;
            }
            else
            {
                _field = null;
            }

            _field = field;
            _value = value;
        }

        public StringCondition(string value)
            : this(null, value)
        {
        }

        public override string Definition
        {
            get
            {
                // TODO: String encode value.

                var format = !string.IsNullOrWhiteSpace(_field)
                    ? "{0}:'{1}'"
                    : "'{1}'";

                return string.Format(format, _field, _value);
            }
        }
    }
}