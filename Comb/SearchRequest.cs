using System.Collections.Generic;

namespace Comb
{
    public class SearchRequest
    {
        public uint? Start { get; set; }
        public uint? Size { get; set; }

        public Query Query { get; set; }

        public List<Sort> Sort { get; set; }

        public List<Return> Return { get; set; }

        public IEnumerable<IExpression> Expressions
        {
            get
            {
                // Search sort for expressions
                foreach (var sort in Sort)
                {
                    var expression = sort.Field as IExpression;
                    if (expression != null)
                        yield return expression;
                }
                // Search return for expressions
                foreach (var ret in Return)
                {
                    var expression = ret.Field as IExpression;
                    if (expression != null)
                        yield return expression;
                }
            }
        }

        public SearchRequest()
        {
            Sort = new List<Sort>();
            Return = new List<Return>();
        }
    }
}