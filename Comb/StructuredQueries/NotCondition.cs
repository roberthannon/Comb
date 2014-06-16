using System;
using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public class NotCondition : GroupCondition
    {
        readonly uint? _boost;
        readonly Condition _term;

        public uint? Boost
        {
            get { return _boost; }
        }

        public override IEnumerable<Option> Options
        {
            get
            {
                if (_boost.HasValue)
                    yield return new Option("boost", _boost.ToString());
            }
        }

        public override IEnumerable<Condition> Terms
        {
            get { yield return _term; }
        }

        public NotCondition(Condition term, uint? boost = null)
            : base("not")
        {
            if (term == null)
                throw new ArgumentNullException("term");

            _boost = boost;
            _term = term;
        }
    }
}