using System;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class RangeTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void CorrectParamsAreIncluded()
        {
            var condition = new Range(3, 7, minInclusive: true);
            var definition = condition.QueryDefinition;

            Assert.That(definition, Is.EqualTo("[3,7}"));
        }

        [Test]
        public void NoLowerBound()
        {
            var condition = new Range(null, new DateValue(new DateTime(2014, 07, 06, 13, 23, 56, 227)));
            var definition = condition.QueryDefinition;

            Assert.That(definition, Is.EqualTo("{,'2014-07-06T13:23:56.227Z'}"));
        }

        [Test]
        public void NoUpperBound()
        {
            var condition = new Range(new DateValue(new DateTime(2014, 07, 06, 13, 23, 56, 227)), maxInclusive: true);
            var definition = condition.QueryDefinition;

            Assert.That(definition, Is.EqualTo("{'2014-07-06T13:23:56.227Z',]"));
        }
    }
}
