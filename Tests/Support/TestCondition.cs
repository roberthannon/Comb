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

        public IReadOnlyList<IOperand> Operands
        {
            get { throw new System.NotImplementedException(); }
        }

        public IReadOnlyList<Option> Options
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
