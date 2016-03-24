using System.Collections.Generic;

namespace Comb
{
    public class MatchAllCondition : IOperator // Special operator, don't inherit from Operator
    {
        public MatchAllCondition()
        {
            Operands = new IOperand[0];
            Options = new Option[0];
        }

        public string Definition => Opcode;

        public string Opcode => "matchall";

        public IReadOnlyList<IOperand> Operands { get; }

        public IReadOnlyList<Option> Options { get; }
    }
}