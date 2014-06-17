﻿using System;
using Comb.StructuredQueries;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class StringConditionTests
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
                new StringCondition(field, "blip");
            },
            Throws.ArgumentException.ForParameter("field"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void NullAndEmptyValuesThrowException(string value)
        {
            Assert.That(() =>
            {
                new StringCondition("field", value);
            },
            Throws.TypeOf<ArgumentNullException>().ForParameter("value"));
        }

        [TestCase("NULL TERMINATED\0", "noop:'NULL TERMINATED'")]
        [TestCase("form\f feed", "noop:'form feed'")]
        [TestCase("\vvertical tab", "noop:'vertical tab'")]
        public void InvalidXmlCharactersAreStrippedFromValue(string value, string expected)
        {
            var condition = new StringCondition("noop", value);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo(expected));
        }

        [Test]
        public void DefinitionIsFieldColonValueInQuotes()
        {
            var condition = new StringCondition("a_1_b", "beep");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("a_1_b:'beep'"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void MissingFieldNamesAreLeftOut(string field)
        {
            var condition = new StringCondition(field, "boop");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("'boop'"));
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
            var condition = new StringCondition("zurb", value);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo(expected));
        }
    }
}