using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class NearConditionTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void CorrectParamsAreIncluded()
        {
            var condition = new NearCondition("some test text", "testfield", 8, 10);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(near field=testfield boost=8 distance=10 'some test text')"));
        }
    }
}
