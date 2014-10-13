using System;

namespace Comb
{
    public class FieldCondition : UniOperator
    {
        FieldCondition(IField field, IOperand operand)
            : base(operand, field)
        {
            if (field == null)
                throw new ArgumentNullException("field");
        }

        public FieldCondition(IField field, StringValue operand)
            : this(field, (IOperand)operand)
        {
        }

        public FieldCondition(IField field, IntValue operand)
            : this(field, (IOperand)operand)
        {
        }

        public FieldCondition(IField field, DoubleValue operand)
            : this(field, (IOperand)operand)
        {
        }

        public FieldCondition(IField field, DateValue operand)
            : this(field, (IOperand)operand)
        {
        }

        public FieldCondition(IField field, Range operand)
            : this(field, (IOperand)operand)
        {
        }

        public FieldCondition(string field, string operand)
            : this(new Field(field), new StringValue(operand))
        {
        }

        public FieldCondition(string field, int operand)
            : this(new Field(field), new IntValue(operand))
        {
        }

        public FieldCondition(string field, double operand)
            : this(new Field(field), new DoubleValue(operand))
        {
        }

        public FieldCondition(string field, DateTime operand)
            : this(new Field(field), new DateValue(operand))
        {
        }

        public FieldCondition(string field, Range operand)
            : this(new Field(field), (IOperand)operand)
        {
        }

        public override string Opcode { get { return null; } }

        public override string QueryDefinition
        {
            get { return string.Format("{0}:{1}", Field.Name, Operand.QueryDefinition); }
        }
    }
}