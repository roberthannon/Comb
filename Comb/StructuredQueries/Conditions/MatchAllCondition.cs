using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public class MatchAllCondition : IOperator
    {
        public string Definition { get { return Opcode; } }

        public string Opcode { get { return "matchall"; } }

        public IEnumerable<Option> Options { get { return Enumerable.Empty<Option>(); } }

        public IEnumerable<IOperand> Operands { get { return Enumerable.Empty<IOperand>(); } }
    }
}