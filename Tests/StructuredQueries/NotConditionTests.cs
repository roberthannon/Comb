using System;
using System.Linq;
using Comb.StructuredQueries;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class NotConditionTests
    {
        [Test]
        public void NullTermThrowsException()
        {
            Assert.That(() =>
            {
                new NotCondition(null);
            },
            Throws.TypeOf<ArgumentNullException>().ForParameter("operand"));
        }

        [Test]
        public void FieldIsAddedToOptions()
        {
            var condition = new NotCondition(new TestCondition("TEST"), field: "testfield");
            var option = condition.Options.Single();

            Assert.That(option, Is.Not.Null);
            Assert.That(option.Name, Is.EqualTo("field"));
            Assert.That(option.Value, Is.EqualTo("testfield"));
        }

        [Test]
        public void NullFieldIsIgnored()
        {
            var condition = new NotCondition(new TestCondition("TEST"));

            Assert.That(condition.Field, Is.Null);
            Assert.That(!condition.Options.Any());
        }

        [Test]
        public void FieldIsAddedToDefinition()
        {
            var condition = new NotCondition(new TestCondition("TEST"), field: "testfield");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(not field=testfield TEST)"));
        }

        [Test]
        public void BoostIsAddedToOptions()
        {
            var condition = new NotCondition(new TestCondition("TEST"), 456);
            var option = condition.Options.Single();

            Assert.That(option, Is.Not.Null);
            Assert.That(option.Name, Is.EqualTo("boost"));
            Assert.That(option.Value, Is.EqualTo("456"));
        }

        [Test]
        public void NullBoostIsIgnored()
        {
            var condition = new NotCondition(new TestCondition("TEST"));
            
            Assert.That(condition.Boost, Is.Null);
            Assert.That(!condition.Options.Any());
        }

        [Test]
        public void BoostIsAddedToDefinition()
        {
            var condition = new NotCondition(new TestCondition("TEST"), 3);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(not boost=3 TEST)"));
        }

        [Test]
        public void TermIsWrapped()
        {
            var condition = new NotCondition(new TestCondition("TEST"));
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(not TEST)"));
        }
    }
}
