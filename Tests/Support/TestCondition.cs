using Comb.StructuredQueries;

namespace Comb.Tests.Support
{
    class TestCondition : IOperand
    {
        public string Definition { get; private set; }

        public TestCondition(string definition)
        {
            Definition = definition;
        }
    }
}
