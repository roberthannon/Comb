using System;

namespace Comb
{
    public class DateValue : IOperand
    {
        readonly DateTime _value;

        public DateValue(DateTime value)
        {
            _value = value;
        }

        public DateTime Value { get { return _value; } }

        public string Definition
        {
            get { return Utilities.WrapValue(ToString()); }
        }

        public override string ToString()
        {
            return Utilities.DateString(_value); // Not wrapped in single quotes
        }
    }
}