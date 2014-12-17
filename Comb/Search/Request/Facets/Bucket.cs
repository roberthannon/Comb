using System;

namespace Comb
{
    public class Bucket
    {
        readonly IOperand _operand;

        private Bucket(IOperand operand)
        {
            if (operand == null)
                throw new ArgumentNullException("operand");

            _operand = operand;
        }

        public Bucket(IntValue value)
            : this((IOperand)value)
        {
        }

        public Bucket(DoubleValue value)
            : this((IOperand)value)
        {
        }

        public Bucket(DateValue value)
            : this((IOperand)value)
        {
        }

        public Bucket(StringValue value)
            : this((IOperand)value)
        {
        }

        public Bucket(Range range)
            : this((IOperand)range) 
        {
        }

        public Bucket(int value)
            : this(new IntValue(value))
        {
        }

        public Bucket(double value)
            : this(new DoubleValue(value))
        {
        }

        public Bucket(DateTime value)
            : this(new DateValue(value))
        {
        }

        public Bucket(string value)
            : this(new StringValue(value))
        {
        }

        public IOperand Operand
        {
            get { return _operand; }
        }

        public string Definition
        {
            get { return _operand.ToString(); } // Operand is NOT wrapped in single quotes in bucket definition
        }
    }
}