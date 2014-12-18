using Comb.Tests.Support;
using NUnit.Framework;
using System;

namespace Comb.Tests.StructuredQueries
{
    public class RangeConditionTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void NullTermsThrowsException()
        {
            Assert.That(() =>
            {
                new RangeCondition(new Range(2,3), (IField)null);
            },
            Throws.TypeOf<ArgumentNullException>().ForParameter("field"));
        }

        [Test]
        public void CorrectParamsAreIncluded()
        {
            var condition = new RangeCondition(new Range(3, 7, minInclusive: true), "testfield");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(range field=testfield [3,7})"));
        }

        [Test]
        public void CorrectDateParamsAreIncluded()
        {
            var condition = new RangeCondition(new Range(new DateTime(2013, 06, 05, 12, 22, 55, 112), new DateTime(2014, 07, 06, 13, 23, 56, 227)), "testfield");
            var definition = condition.Definition;

            Assert.AreEqual(definition, "(range field=testfield {'2013-06-05T12:22:55.112Z','2014-07-06T13:23:56.227Z'})");
        }

        [Test]
        public void CorrectLatLonParamsAreIncluded()
        {
            var condition = new RangeCondition(new Range(new LatLon(-1.0, -2.01), new LatLon(-55.033, 77)), "testfield");
            var definition = condition.Definition;

            Assert.AreEqual(definition, "(range field=testfield {'-1,-2.01','-55.033,77'})");
        }
    }
}
