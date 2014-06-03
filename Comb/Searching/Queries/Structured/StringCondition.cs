namespace Comb.Searching.Queries.Structured
{
    public class StringCondition : Condition
    {
        readonly string _text;

        public StringCondition(string text)
        {
            _text = text;
        }

        public override string Definition
        {
            get
            {
                // TODO: String encode this.
                return string.Format("'{0}'", _text);
            }
        }
    }
}