using System.Collections.Generic;

namespace Comb.Tests.Support
{
    class TestCondition : IOperator
    {
        public string QueryDefinition { get; private set; }

        public TestCondition(string definition)
        {
            QueryDefinition = definition;
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
