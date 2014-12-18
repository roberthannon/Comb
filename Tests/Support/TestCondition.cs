using System.Collections.Generic;

namespace Comb.Tests.Support
{
    class TestCondition : IOperator
    {
        public string Definition { get; private set; }

        public TestCondition(string definition)
        {
            Definition = definition;
        }

        public string Opcode
        {
            get { throw new System.NotImplementedException(); }
        }

        public ICollection<Option> Options
        {
            get { throw new System.NotImplementedException(); }
        }

        public ICollection<IOperand> Operands
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
