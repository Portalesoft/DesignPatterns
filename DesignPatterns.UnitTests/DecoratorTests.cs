using DesignPatterns.Patterns.Decorator;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Decorator Design Pattern
    /// </summary>
    [TestFixture]
    internal class DecoratorTests {

        /// <summary>
        /// Simple decorator implementation of sealed StringBuilder
        /// </summary>
        [Test]
        public void CodeBuilder_ShouldAppendText() {
            var cb = new CodeBuilder();
            cb.Append("Hello World");
            Assert.That(cb.ToString(), Is.EqualTo("Hello World"));
        }

    }

}
