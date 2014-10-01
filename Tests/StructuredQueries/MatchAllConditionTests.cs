using Comb.StructuredQueries;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class MatchAllConditionTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void CorrectParamsAreIncluded()
        {
            var condition = new MatchAllCondition();
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("matchall"));
        }
    }
}
