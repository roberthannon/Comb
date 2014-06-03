namespace Comb.Searching.Queries.Structured
{
    public class StringCondition : Condition
    {
        readonly string _field;
        readonly string _value;

        public StringCondition(string field, string value)
        {
            _field = field;
            _value = value;
        }

        public StringCondition(string value)
        {
            _value = value;
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