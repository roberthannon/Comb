using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public class OrCondition : Operator
    {
        public OrCondition(ICollection<IOperand> operands, IField field = null, uint? boost = null)
            : base(operands, field, boost)
        {
            if (operands.Any(o => !(o is IOperator || o is StringValue)))
                throw new ArgumentException("Invalid operands: only IOperator and StringValue instances are allowed", "operands");
        }

        public OrCondition(ICollection<IOperand> operands, string field, uint? boost = null)
            : this(operands, new Field(field), boost)
        {
        }

        public OrCondition(params IOperand[] operands)
            : this(operands, (IField)null)
        {
        }

        public override string Opcode { get { return "or"; } }
    }
}