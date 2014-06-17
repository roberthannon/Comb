using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public class PrefixCondition : GroupCondition
    {
        readonly string _field;
        readonly string _value;

        public PrefixCondition(string field, string value)
            : base("prefix")
        {
            _field = field;
            _value = value;
        }

        public override IEnumerable<Option> Options
        {
            get { yield return new Option("field", _field); }
        }

        public override IEnumerable<ICondition> Terms
        {
            get { yield return new StringCondition(_value); }
        }
    }
}
