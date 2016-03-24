using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb
{
    public abstract class Operator : IOperator
    {
        readonly List<Option> _options;

        protected Operator(IEnumerable<IOperand> operands, IField field = null, uint? boost = null)
        {
            if (operands == null) throw new ArgumentNullException(nameof(operands));

            Operands = operands.ToList(); // Make a copy of operands

            if (Operands.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(operands), "An Operator must have at least one operand.");

            Field = field;
            Boost = boost;

            _options = new List<Option>();

            if (Field != null) AddOption("field", Field.Name);
            if (Boost.HasValue) AddOption("boost", Boost.ToString());
        }

        public abstract string Opcode { get; }

        public IReadOnlyList<IOperand> Operands { get; }

        public IField Field { get; }

        public uint? Boost { get; }

        public IReadOnlyList<Option> Options => _options;

        public virtual string Definition
        {
            get
            {
                var components = new List<string> { Opcode };
                components.AddRange(Options.Select(o => o.Definition));
                components.AddRange(Operands.Select(o => o.Definition));
                return $"({string.Join(" ", components)})";
            }
        }

        protected void AddOption(string name, string value)
        {
            _options.Add(new Option(name, value));
        }
    }
}