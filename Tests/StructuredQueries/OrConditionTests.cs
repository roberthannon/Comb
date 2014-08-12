using System;
using System.Collections.ObjectModel;
using System.Linq;
using Comb.StructuredQueries;
using Comb.Tests.Support;
using NUnit.Framework;

namespace Comb.Tests.StructuredQueries
{
    public class OrConditionTests
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
                new OrCondition(null);
            },
            Throws.TypeOf<ArgumentNullException>().ForParameter("operands"));
        }

        [Test]
        public void EmptyTermsThrowsException()
        {
            Assert.That(() =>
            {
                new OrCondition(new Collection<IOperand>());
            },
            Throws.TypeOf<ArgumentOutOfRangeException>().ForParameter("operands"));
        }

        [Test]
        public void BoostIsAddedToOptions()
        {
            var condition = new OrCondition(new Collection<IOperand> { new TestCondition("TEST") }, 984);
            var option = condition.Options.Single();

            Assert.That(option, Is.Not.Null);
            Assert.That(option.Name, Is.EqualTo("boost"));
            Assert.That(option.Value, Is.EqualTo("984"));
        }

        [Test]
        public void NullBoostIsIgnored()
        {
            var condition = new OrCondition(new Collection<IOperand> { new TestCondition("TEST") });

            Assert.That(condition.Boost, Is.Null);
            Assert.That(!condition.Options.Any());
        }

        [Test]
        public void FieldIsAddedToOptions()
        {
            var condition = new OrCondition(new Collection<IOperand> { new TestCondition("TEST") }, field: "testfield");
            var option = condition.Options.Single();

            Assert.That(option, Is.Not.Null);
            Assert.That(option.Name, Is.EqualTo("field"));
            Assert.That(option.Value, Is.EqualTo("testfield"));
        }

        [Test]
        public void NullFieldIsIgnored()
        {
            var condition = new OrCondition(new Collection<IOperand> { new TestCondition("TEST") });

            Assert.That(condition.Field, Is.Null);
            Assert.That(!condition.Options.Any());
        }

        [Test]
        public void FieldIsAddedToDefinition()
        {
            var condition = new OrCondition(new Collection<IOperand> { new TestCondition("TEST") }, field: "testfield");
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(or field=testfield TEST)"));
        }

        [Test]
        public void BoostIsAddedToDefinition()
        {
            var condition = new OrCondition(new Collection<IOperand> { new TestCondition("TEST") }, 8);
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(or boost=8 TEST)"));
        }

        [Test]
        public void OneTermIsWrapped()
        {
            var condition = new OrCondition(new Collection<IOperand> { new TestCondition("TEST") });
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(or TEST)"));
        }

        [Test]
        public void ManyTermsAreWrapped()
        {
            var condition = new OrCondition(new Collection<IOperand>
            {
                new TestCondition("(omg)"),
                new TestCondition("(its (a) (test))"),
                new TestCondition("ZUBB")
            });
            var definition = condition.Definition;

            Assert.That(definition, Is.EqualTo("(or (omg) (its (a) (test)) ZUBB)"));
        }
    }
}
