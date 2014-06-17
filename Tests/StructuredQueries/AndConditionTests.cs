using System;
using System.Collections.ObjectModel;
using System.Linq;
using Comb.StructuredQueries;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class AndConditionTests
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
                new AndCondition(null);
            },
            Throws.TypeOf<ArgumentNullException>().ForParameter("terms"));
        }

        [Test]
        public void EmptyTermsThrowsException()
        {
            Assert.That(() =>
            {
                new AndCondition(new Collection<ICondition>());
            },
            Throws.TypeOf<ArgumentOutOfRangeException>().ForParameter("terms"));
        }

        [Test]
        public void BoostIsAddedToOptions()
        {
            var condition = new AndCondition(new Collection<ICondition> { new TestCondition("TEST") }, 984);
            var option = condition.Options.Single();

            Assert.That(option, Is.Not.Null);
            Assert.That(option.Name, Is.EqualTo("boost"));
            Assert.That(option.Value, Is.EqualTo("984"));
        }

        [Test]
        public void NullBoostIsIgnored()
        {
            var condition = new AndCondition(new Collection<ICondition> { new TestCondition("TEST") });

            Assert.That(condition.Boost, Is.Null);
            Assert.That(!condition.Options.Any());
        }

        [Test]
        public void BoostIsAddedToDefinition()
        {
            var condition = new AndCondition(new Collection<ICondition> { new TestCondition("TEST") }, 8);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(and boost=8 TEST)"));
        }

        [Test]
        public void OneTermIsWrapped()
        {
            var condition = new AndCondition(new Collection<ICondition> { new TestCondition("TEST") });
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(and TEST)"));
        }

        [Test]
        public void ManyTermsAreWrapped()
        {
            var condition = new AndCondition(new Collection<ICondition>
            {
                new TestCondition("(omg)"),
                new TestCondition("(its (a) (test))"),
                new TestCondition("ZUBB")
            });
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(and (omg) (its (a) (test)) ZUBB)"));
        }
    }
}
