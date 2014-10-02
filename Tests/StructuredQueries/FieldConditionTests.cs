using System;
using Comb.StructuredQueries;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class FieldConditionTests
    {
        [TestCase("%")]
        [TestCase("a1")]
        [TestCase(" a ")]
        [TestCase("\f\0\f")]
        [TestCase("score")]
        public void BadFieldNamesThrowException(string field)
        {
            Assert.That(() =>
            {
                new FieldCondition(field, "blip");
            },
            Throws.ArgumentException.ForParameter("name"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void NullAndEmptyValuesThrowException(string value)
        {
            Assert.That(() =>
            {
                new FieldCondition("field", value);
            },
            Throws.TypeOf<ArgumentNullException>().ForParameter("value"));
        }

        [TestCase("NULL TERMINATED\0", "noop:'NULL TERMINATED'")]
        [TestCase("form\f feed", "noop:'form feed'")]
        [TestCase("\vvertical tab", "noop:'vertical tab'")]
        public void InvalidXmlCharactersAreStrippedFromValue(string value, string expected)
        {
            var condition = new FieldCondition("noop", value);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo(expected));
        }

        [Test]
        public void DefinitionIsFieldColonValueInQuotes()
        {
            var condition = new FieldCondition("a_1_b", "beep");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("a_1_b:'beep'"));
        }

        [TestCase("/", "zurb:'/'")]
        [TestCase("\"", "zurb:'\"'")]
        [TestCase("&", "zurb:'&'")]
        [TestCase("<", "zurb:'<'")]
        [TestCase(">", "zurb:'>'")]
        [TestCase("\\", "zurb:'\\\\'")]
        [TestCase("'", "zurb:'\\''")]
        [TestCase("'\\\f/", "zurb:'\\'\\\\/'")]
        public void ValuesAreEncodedIfRequired(string value, string expected)
        {
            var condition = new FieldCondition("zurb", value);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo(expected));
        }

        [Test]
        public void FieldIncludesRangeQuery()
        {
            var condition =  new FieldCondition("testfield", new RangeValue(33, 123, false, true));
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("testfield:{33,123]"));
        }
    }
}