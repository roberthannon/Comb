using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class PhraseConditionTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void CorrectParamsAreIncluded()
        {
            var condition = new PhraseCondition("some test text", "testfield", 6);
            var definition = condition.QueryDefinition;

            Assert.That(definition, Is.EqualTo("(phrase field=testfield boost=6 'some test text')"));
        }
    }
}
