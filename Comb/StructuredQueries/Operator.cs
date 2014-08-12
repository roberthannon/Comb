using System;
using System.Collections.Generic;
using System.Linq;

namespace Comb.StructuredQueries
{
    public abstract class Operator : IOperator
    {
        protected Operator(string opcode)
        {
            if (opcode == null)
                throw new ArgumentNullException("opcode");

            Opcode = opcode;
        }

        protected Operator(string opcode, params IOperand[] operands)
            : this(opcode)
        {
            if (operands == null)
                throw new ArgumentNullException("operands");

            if (operands.Length < 1)
                throw new ArgumentOutOfRangeException("operands", "An Operator must have at least one operand.");

            Operands = operands;
        }

        protected Operator(string opcode, ICollection<IOperand> operands)
            : this(opcode)
        {
            if (operands == null)
                throw new ArgumentNullException("operands");

            if (!operands.Any())
                throw new ArgumentOutOfRangeException("operands", "An Operator must have at least one operand.");

            Operands = operands;
        }

        public string Opcode { get; private set; }

        public string Field { get; protected set; }

        public uint? Boost { get; protected set; }

        public IEnumerable<Option> Options
        {
            get
            {
                if (!string.IsNullOrEmpty(Field))
                    yield return new Option("field", Field);

                if (Boost.HasValue)
                    yield return new Option("boost", Boost.ToString());
            }
        }

        public IEnumerable<IOperand> Operands { get; protected set; }

        public string Definition
        {
            get
            {
                var parts = new List<string>
                {
                    Opcode
                };

                foreach (var option in Options)
                    parts.Add(string.Format("{0}={1}", option.Name, option.Value));

                foreach (var operand in Operands)
                    parts.Add(operand.Definition);

                return string.Format("({0})", string.Join(" ", parts));
            }
        }
    }
}