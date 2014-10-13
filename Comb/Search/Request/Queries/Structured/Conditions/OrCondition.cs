using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb
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

        public override string Opcode { get { return "or"; } }

        public override string QueryDefinition
        {
            get
            {
                return Operands.Count == 1 && !Options.Any() ? // If and condition has one operand and no options, it is a no-op
                    Operands.Single().QueryDefinition : // Return the definition of the only operand
                    base.QueryDefinition;
            }
        }
    }
}