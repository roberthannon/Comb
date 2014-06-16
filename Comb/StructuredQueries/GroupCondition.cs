using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public abstract class GroupCondition : ICondition
    {
        protected GroupCondition(string operatar)
        {
            Operator = operatar;
        }

        public string Operator { get; private set; }

        public abstract IEnumerable<Option> Options { get; }

        public abstract IEnumerable<ICondition> Terms { get; }

        public string Definition
        {
            get
            {
                var parts = new List<string>
                {
                    Operator
                };

                foreach (var option in Options)
                    parts.Add(string.Format("{0}={1}", option.Name, option.Value));

                foreach (var term in Terms)
                    parts.Add(term.Definition);

                return string.Format("({0})", string.Join(" ", parts));
            }
        }
    }
}