using System;
using System.Linq;
using System.Security.Permissions;
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
            Throws.TypeOf<ArgumentNullException>().ForParameter("term"));
        }

        [Test]
        public void BoostIsAddedToOptions()
        {
            var condition = new NotCondition(new TestCondition(), 456);
            var option = condition.Options.Single();

            Assert.That(option, Is.Not.Null);
            Assert.That(option.Name, Is.EqualTo("boost"));
            Assert.That(option.Value, Is.EqualTo("456"));
        }

        [Test]
        public void NullBoostIsIgnored()
        {
            var condition = new NotCondition(new TestCondition());
            
            Assert.That(condition.Boost, Is.Null);
            Assert.That(!condition.Options.Any());
        }

        [Test]
        public void TermIsWrapped()
        {
            var condition = new NotCondition(new TestCondition());
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(not TEST)"));
        }

        [Test]
        public void BoostIsAddedToDefinition()
        {
            var condition = new NotCondition(new TestCondition(), 3);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(not boost=3 TEST)"));
        }

        class TestCondition : ICondition
        {
            public string Definition { get { return "TEST"; } }
        }
    }
}
