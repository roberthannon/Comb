using System.Collections.Generic;

namespace Comb.Searching.Queries.Structured
{
    public abstract class GroupCondition : Condition
    {
        protected GroupCondition(string operatar)
        {
            Operator = operatar;
        }

        public string Operator { get; private set; }

        public abstract IEnumerable<Option> Options { get; }

        public abstract IEnumerable<Condition> Terms { get; }

        public override string Definition
        {
            get
            {
                var parts = new List<string>
                {
                    Operator
                };

                foreach (var term in Terms)
                    parts.Add(term.Definition);

                return string.Format("({0})", string.Join(" ", parts));
            }
        }
    }
}