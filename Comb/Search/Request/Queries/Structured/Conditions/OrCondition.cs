using System.Collections.Generic;
using System.Linq;

namespace Comb
{
    public class OrCondition : Operator
    {
        public OrCondition(ICollection<IOperator> operands, IField field = null, uint? boost = null)
            : base(operands, field, boost)
        {
        }

        public OrCondition(ICollection<IOperator> operands, string field, uint? boost = null)
            : this(operands, new Field(field), boost)
        {
        }

        public override string Opcode { get { return "or"; } }

        public override string Definition
        {
            get
            {
                return Operands.Count == 1 && Options.Count == 0 ? // If or condition has one operand and no options, it is a no-op
                    Operands.Single().Definition : // Return the definition of the only operand
                    base.Definition;
            }
        }
    }
}