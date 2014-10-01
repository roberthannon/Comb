using System;

namespace Comb.StructuredQueries
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
            get { return string.Format("'{0}'", Value.ToString(Constants.DateFormat)); }
        }
    }
}