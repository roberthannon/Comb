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
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("[3,7}"));
        }

        [Test]
        public void NoLowerBound()
        {
            var condition = new Range(null, new DateTime(2014, 07, 06, 13, 23, 56, 227));
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("{,'2014-07-06T13:23:56.227Z'}"));
        }

        [Test]
        public void NoUpperBound()
        {
            var condition = new Range(new DateTime(2014, 07, 06, 13, 23, 56, 227), maxInclusive: true);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("{'2014-07-06T13:23:56.227Z',]"));
        }

        [Test]
        public void CorrectLatLonParamsAreIncluded()
        {
            var condition = new Range(null, new LatLon(-55.033, 77.1123), maxInclusive: true);
            var definition = condition.Definition;

            Assert.AreEqual(definition, "{,'-55.033,77.1123']");
        }
    }
}
