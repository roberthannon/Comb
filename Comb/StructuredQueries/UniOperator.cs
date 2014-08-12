using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public abstract class UniOperator : Operator
    {
        protected UniOperator(string opcode)
            : base(opcode)
        {
        }

        protected UniOperator(string opcode, IOperand operand)
            : base(opcode, operand)
        {
            if (operand == null)
                throw new ArgumentNullException("operand");
        }

        public IOperand Operand { get { return Operands.FirstOrDefault(); } }
    }
}