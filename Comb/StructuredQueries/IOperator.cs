using System.Collections.Generic;

namespace Comb.StructuredQueries
{
    public interface IOperator : IOperand
    {
        string Opcode { get; }

        IEnumerable<Option> Options { get; }

        IEnumerable<IOperand> Operands { get; }
    }
}