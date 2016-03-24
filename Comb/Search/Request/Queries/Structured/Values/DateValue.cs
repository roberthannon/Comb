using System;

namespace Comb
{
    public class DateValue : IOperand
    {
        public DateValue(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }

        public string Definition => Utilities.WrapValue(ToString());

        public override string ToString()
        {
            return Utilities.DateString(Value); // Not wrapped in single quotes
        }
    }
}