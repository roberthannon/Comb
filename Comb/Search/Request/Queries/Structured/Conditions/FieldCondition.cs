using System;

namespace Comb
{
    public class FieldCondition : UniOperator
    {
        public FieldCondition(StringValue operand, IField field = null)
            : base(operand, field)
        {
        }

        public FieldCondition(IntValue operand, IField field = null)
            : base(operand, field)
        {
        }

        public FieldCondition(DoubleValue operand, IField field = null)
            : base(operand, field)
        {
        }

        public FieldCondition(DateValue operand, IField field = null)
            : base(operand, field)
        {
        }

        public FieldCondition(Range operand, IField field)
            : base(operand, field)
        {
            // Field can't be null when using a range
            if (field == null)
                throw new ArgumentNullException(nameof(field));
        }

        public FieldCondition(string operand, string field = null)
            : this(new StringValue(operand), field != null ? new Field(field) : null)
        {
        }

        public FieldCondition(int operand, string field = null)
            : this(new IntValue(operand), field != null ? new Field(field) : null)
        {
        }

        public FieldCondition(double operand, string field = null)
            : this(new DoubleValue(operand), field != null ? new Field(field) : null)
        {
        }

        public FieldCondition(DateTime operand, string field = null)
            : this(new DateValue(operand), field != null ? new Field(field) : null)
        {
        }

        public FieldCondition(Range operand, string field)
            : this(operand, new Field(field))
        {
        }

        public override string Opcode => null;

        public override string Definition => Field == null ? Operand.Definition : $"{Field.Name}:{Operand.Definition}";
    }
}