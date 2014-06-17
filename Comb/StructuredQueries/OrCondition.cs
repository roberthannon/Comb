using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public class OrCondition : GroupCondition
    {
        readonly uint? _boost;
        readonly ICollection<ICondition> _terms;

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

        public override IEnumerable<ICondition> Terms
        {
            get { return _terms; }
        }

        public OrCondition(ICollection<ICondition> terms, uint? boost = null)
            : base("or")
        {
            if (terms == null)
                throw new ArgumentNullException("terms");

            if (!terms.Any())
                throw new ArgumentOutOfRangeException("terms", "An OrCondition must have at least one term.");

            _boost = boost;
            _terms = terms;
        }
    }
}