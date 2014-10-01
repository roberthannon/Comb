using System;
using Comb.StructuredQueries;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class RangeConditionTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void CorrectParamsAreIncluded()
        {
            var condition = new RangeCondition(new IntValue(3), new IntValue(7), true, false);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("[3,7}"));
        }

        [Test]
        public void IgnoresNullValue()
        {
            var condition = new RangeCondition(null, new DateValue(new DateTime(2014, 07, 06, 13, 23, 56, 227)));
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("{,'2014-07-06T13:23:56.227Z'}"));
        }
    }
}
