using System.Collections.Generic;

namespace Comb
{
    public class MatchAllCondition : IOperator // Special operator, don't inherit from Operator
    {
        readonly IOperand[] _operands = new IOperand[0];
        readonly Option[] _options = new Option[0];

        public string Definition { get { return Opcode; } }

        public string Opcode { get { return "matchall"; } }

        public ICollection<IOperand> Operands { get { return _operands; } }

        public ICollection<Option> Options { get { return _options; } }
    }
}