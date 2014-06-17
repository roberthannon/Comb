using System;
using System.Text.RegularExpressions;

namespace Comb.StructuredQueries
{
    public class StringCondition : ICondition
    {
        readonly string _field;
        readonly string _value;

        public StringCondition(string field, string value)
        {
            if (!string.IsNullOrEmpty(field))
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

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value",
                    "Searching for null or empty fields in CloudSearch does not match null and " +
                    "empty fields like you probably expect. We recommend indexing an explicit " +
                    "value like \"NULL\" if you want to be able to search for it explicitly.");
            }

            _field = field;
            _value = value;
        }

        public StringCondition(string value)
            : this(null, value)
        {
        }

        public string Definition
        {
            get
            {
                var format = !string.IsNullOrWhiteSpace(_field)
                    ? "{0}:'{1}'"
                    : "'{1}'";

                return string.Format(format, _field, EncodeValue(_value));
            }
        }

        string EncodeValue(string value)
        {
            /*
             * Both JSON and XML batches can only contain UTF-8 characters that are valid in XML.
             * Valid characters are the control characters tab (0009), carriage return (000D),
             * and line feed (000A), and the legal characters of Unicode and ISO/IEC 10646. FFFE,
             * FFFF, and the surrogate blocks D800–DBFF and DC00–DFFF are invalid and will cause
             * errors. (For more information, see Extensible Markup Language (XML) 1.0 (Fifth
             * Edition).) You can use the following regular expression to match invalid characters
             * so you can remove them: /[^\u0009\u000a\u000d\u0020-\uD7FF\uE000-\uFFFD]/ .
             * */
            value = Regex.Replace(value, "[^\u0009\u000a\u000d\u0020-\uD7FF\uE000-\uFFFD]", "");
            value = value.Replace("\\", "\\\\");
            value = value.Replace("'", "\\'");

            return value;
        }
    }
}