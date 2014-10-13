using System;

namespace Comb
{
    public class DateValue : IValue
    {
        readonly DateTime _value;

        public DateValue(DateTime value)
        {
            _value = value;
        }

        public DateTime Value { get { return _value; } }

        public string QueryDefinition
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return string.Format("'{0}'", Value.ToString(Constants.DateFormat)); // Always wrapped in single quotes?
        }
    }
}