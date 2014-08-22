using System;
using System.Text.RegularExpressions;

namespace Comb.StructuredQueries
{
    public class StringCondition : IOperand
    {
        public StringCondition(IField field, string value)
        {
            if (field != null && !string.IsNullOrEmpty(field.Name))
            {
                if (!Regex.IsMatch(field.Name, Constants.FieldNameFormat))
                    throw new ArgumentException(string.Format("Invalid field name: {0}", field.Name), "field");

                if (Constants.ReservedFieldNames.Contains(field.Name))
                    throw new ArgumentException(string.Format("Reserved field name: {0}", field.Name), "field");

                Field = field;
            }
            else
            {
                Field = null;
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value",
                    "Searching for null or empty fields in CloudSearch does not match null and " +
                    "empty fields like you probably expect. We recommend indexing an explicit " +
                    "value like \"NULL\" if you want to be able to search for it explicitly.");
            }

            Value = value;
        }

        public StringCondition(string fieldName, string value)
            : this(new Field(fieldName), value)
        {
        }

        public StringCondition(string value)
            : this((IField)null, value)
        {
        }

        public IField Field { get; private set; }

        public string Value { get; private set; }

        public string Definition
        {
            get
            {
                if (Field != null && !string.IsNullOrWhiteSpace(Field.Name))
                    return string.Format("{0}:'{1}'", Field.Name, EncodeValue(Value));
                return string.Format("'{0}'", EncodeValue(Value));
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