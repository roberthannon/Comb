using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public class MatchAllCondition : IOperator
    {
        public string Definition { get { return Opcode; } }

        public string Opcode { get { return "matchall"; } }

        public ICollection<Option> Options { get { return new Option[0]; } }

        public ICollection<IOperand> Operands { get { return new IOperand[0]; } }
    }
}