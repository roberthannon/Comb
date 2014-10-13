using System.Collections.Generic;

namespace Comb
{
    public interface IOperator : IOperand
    {
        string Opcode { get; }

        ICollection<Option> Options { get; }

        ICollection<IOperand> Operands { get; }
    }
}