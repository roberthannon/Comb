using Comb.StructuredQueries;
using Comb.Tests.Support;
using NUnit.Framework;
using System;

namespace Comb.Tests.StructuredQueries
{
    public class TermConditionTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void NullFieldThrowsException()
        {
            Assert.That(() =>
            {
                new TermCondition(new IntValue(123), null);
            },
            Throws.TypeOf<ArgumentNullException>().ForParameter("field"));
        }

        [Test]
        public void CorrectParamsAreIncluded()
        {
            var condition = new TermCondition("some test text", "testfield", 2);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(term field=testfield boost=2 'some test text')"));
        }

        [Test]
        public void CorrectParamsAreIncludedForIntValue()
        {
            var condition = new TermCondition(27, "blahblah", 2);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(term field=blahblah boost=2 27)"));
        }
    }
}
