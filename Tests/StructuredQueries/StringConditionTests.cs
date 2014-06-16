using Comb.StructuredQueries;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class StringConditionTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void MissingFieldNamesAreLeftOut(string field)
        {
            var condition = new StringCondition(field, "boop");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("'boop'"));
        }

        [TestCase("%")]
        [TestCase("a1")]
        [TestCase("score")]
        public void BadFieldNamesThrowException(string field)
        {
            Assert.That(() =>
            {
                new StringCondition(field, "blip");
            },
                Throws.ArgumentException.ForParameter("field"));
        }

        [TestCase("one",    "one:'beep'")]
        [TestCase("a_1_b",  "a_1_b:'beep'")]
        public void FieldNameGoesFirst(string field, string expected)
        {
            var condition = new StringCondition(field, "beep");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo(expected));
        }

        // TODO: Null values?
        // TODO: Encoding?
    }
}