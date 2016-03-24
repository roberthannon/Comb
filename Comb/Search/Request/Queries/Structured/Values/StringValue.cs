using System;

namespace Comb
{
    public class StringValue : IOperand
    {
        public StringValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value),
                    "Searching for null or empty fields in CloudSearch does not match null and " +
                    "empty fields like you probably expect. We recommend indexing an explicit " +
                    "value like \"NULL\" if you want to be able to search for it explicitly.");
            }

            Value = value;
        }

        public string Value { get; }

        public string Definition => Utilities.WrapValue(ToString());

        public override string ToString()
        {
            return Utilities.EncodeValue(Value); // Not wrapped in single quotes
        }
    }
}