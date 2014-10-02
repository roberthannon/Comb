using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public interface IOperator : IOperand
    {
        string Opcode { get; }

        ICollection<Option> Options { get; }

        ICollection<IOperand> Operands { get; }
    }
}