using System.Collections.Generic;

namespace Comb
{
    public interface IOperator : IOperand
    {
        string Opcode { get; }

        IReadOnlyList<IOperand> Operands { get; }

        IReadOnlyList<Option> Options { get; }
    }
}