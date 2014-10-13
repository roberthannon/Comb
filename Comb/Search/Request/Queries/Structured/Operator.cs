using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb
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

        public virtual string QueryDefinition
        {
            get
            {
                var components = new List<string> { Opcode };
                components.AddRange(Options.Select(o => o.QueryDefinition));
                components.AddRange(Operands.Select(o => o.QueryDefinition));
                return string.Format("({0})", string.Join(" ", components));
            }
        }
    }
}