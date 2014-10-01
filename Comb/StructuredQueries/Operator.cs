using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public abstract class Operator : IOperator
    {
        readonly IField _field;
        readonly uint? _boost;
        readonly ICollection<IOperand> _operands;

        protected Operator(ICollection<IOperand> operands, IField field = null, uint? boost = null)
        {
            if (operands == null)
                throw new ArgumentNullException("operands");

            if (!operands.Any())
                throw new ArgumentOutOfRangeException("operands", "An Operator must have at least one operand.");

            _operands = operands;
            _field = field;
            _boost = boost;
        }

        public abstract string Opcode { get; }

        public IField Field { get { return _field; } }

        public uint? Boost { get { return _boost; } }

        public virtual IEnumerable<Option> Options
        {
            get
            {
                if (Field != null)
                    yield return new Option("field", Field.Name);
                if (Boost.HasValue)
                    yield return new Option("boost", Boost.ToString());
            }
        }

        public IEnumerable<IOperand> Operands { get { return _operands; } }

        public virtual string Definition
        {
            get
            {
                var parts = new List<string> { Opcode };
                parts.AddRange(Options.Select(o => string.Format("{0}={1}", o.Name, o.Value)));
                parts.AddRange(Operands.Select(o => o.Definition));
                return string.Format("({0})", string.Join(" ", parts));
            }
        }
    }
}