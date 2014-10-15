using System;
using System.Text.RegularExpressions;

namespace Comb
{
    public class StringValue : IValue
    {
        readonly string _value;

        public StringValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value",
                    "Searching for null or empty fields in CloudSearch does not match null and " +
                    "empty fields like you probably expect. We recommend indexing an explicit " +
                    "value like \"NULL\" if you want to be able to search for it explicitly.");
            }

            _value = value;
        }

        public string Value { get { return _value; } }

        public string QueryDefinition
        {
            get { return string.Format("'{0}'", this); }
        }

        public override string ToString()
        {
            return Utilities.EncodeValue(Value); // Sometimes want a string value not wrapped in single quotes
        }
    }
}