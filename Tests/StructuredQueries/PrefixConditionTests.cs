using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class PrefixConditionTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void CorrectParamsAreIncluded()
        {
            var condition = new PrefixCondition("some test text", "testfield", 6);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(prefix field=testfield boost=6 'some test text')"));
        }
    }
}
