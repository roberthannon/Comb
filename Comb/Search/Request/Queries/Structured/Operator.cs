using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb
{
    public abstract class Operator : IOperator
    {
        readonly ICollection<IOperand> _operands;
        readonly ICollection<Option> _options;
        readonly IField _field;
        readonly uint? _boost;

        protected Operator(IEnumerable<IOperand> operands, IField field = null, uint? boost = null)
        {
            if (operands == null)
                throw new ArgumentNullException("operands");

            _operands = operands.ToList(); // Make a copy of operands

            if (_operands.Count == 0)
                throw new ArgumentOutOfRangeException("operands", "An Operator must have at least one operand.");

            _options = new List<Option>();
            _field = field;
            _boost = boost;

            if (Field != null) AddOption("field", Field.Name);
            if (Boost.HasValue) AddOption("boost", Boost.ToString());
        }

        public abstract string Opcode { get; }

        public ICollection<IOperand> Operands { get { return _operands; } }

        public ICollection<Option> Options { get { return _options; } }

        public IField Field { get { return _field; } }

        public uint? Boost { get { return _boost; } }

        public virtual string Definition
        {
            get
            {
                var components = new List<string> { Opcode };
                components.AddRange(_options.Select(o => o.Definition));
                components.AddRange(_operands.Select(o => o.Definition));
                return string.Format("({0})", string.Join(" ", components));
            }
        }

        protected Option AddOption(string name, string value)
        {
            var option = new Option(name, value);

            // TODO Allow duplicate options with same name?
            _options.Add(option);

            return option;
        }
    }
}