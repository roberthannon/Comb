using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public abstract class Operator : IOperator
    {
        readonly ICollection<IOperand> _operands;
        protected readonly ICollection<Option> _options;
        readonly IField _field;
        readonly uint? _boost;

        protected Operator(ICollection<IOperand> operands, IField field = null, uint? boost = null)
        {
            if (operands == null)
                throw new ArgumentNullException("operands");

            if (!operands.Any())
                throw new ArgumentOutOfRangeException("operands", "An Operator must have at least one operand.");

            _operands = operands;
            _field = field;
            _boost = boost;

            _options = new List<Option>();
            if (Field != null) _options.Add(new Option("field", Field.Name));
            if (Boost.HasValue) _options.Add(new Option("boost", Boost.ToString()));
        }

        public abstract string Opcode { get; }

        public IField Field { get { return _field; } }

        public uint? Boost { get { return _boost; } }

        public ICollection<Option> Options { get { return _options; } }

        public ICollection<IOperand> Operands { get { return _operands; } }

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