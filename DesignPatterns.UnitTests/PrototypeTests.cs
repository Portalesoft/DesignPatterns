using DesignPatterns.Patterns.Builder;
using DesignPatterns.Patterns.Prototype;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Prototype Design Pattern
    /// </summary>
    [TestFixture]
    internal class PrototypeTests {

        /// <summary>
        /// Prototype Deep Copy XML Test
        /// </summary>
        [Test]
        public void DeepCopyXml_ShouldDeepCopyPerson() {
            var barry = Person.New
                .Called("Barry")
                .WorksAsA("Developer")
                .Build();

            var john = barry.DeepCopyXml();
            Assert.That(john.Name, Is.EqualTo("Barry"));
            Assert.That(john.Position, Is.EqualTo("Developer"));
        }
    }
}
