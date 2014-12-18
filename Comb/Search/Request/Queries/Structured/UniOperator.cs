using System;
using System.Linq;

namespace Comb
{
    /// <summary>
    /// Several operators only take one operand. Convenient base class for those operators.
    /// </summary>
    public abstract class UniOperator : Operator
    {
        protected UniOperator(IOperand operand, IField field = null, uint? boost = null)
            : base(new[] { operand }, field, boost)
        {
            if (operand == null)
                throw new ArgumentNullException("operand");
        }

        public IOperand Operand { get { return Operands.FirstOrDefault(); } }
    }
}