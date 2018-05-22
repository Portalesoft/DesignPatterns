using DesignPatterns.Patterns.Visitor;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Visitor Design Pattern
    /// </summary>
    [TestFixture]
    internal class VisitorTests {

        private AdditionExpression _expression;

        /// <summary>
        /// Initialisation for unit tests
        /// </summary>
        [SetUp]
        public void SetUp() {
            _expression = new AdditionExpression(
                new DoubleExpression(1),
                new AdditionExpression(
                    new DoubleExpression(2),
                    new DoubleExpression(3)));
        }

        /// <summary>
        /// Expression printer 
        /// </summary>
        [Test]
        public void ExpressionPrinter_ShouldPrintExpression() {
            var ep = new ExpressionPrinter();
            ep.Visit(_expression);
            Assert.That(ep.ToString(), Is.EqualTo("(1+(2+3))"));
        }

        /// <summary>
        /// Expression calculator 
        /// </summary>
        [Test]
        public void ExpressionCalculator_ShouldCalculateExpression() {
            var calc = new ExpressionCalculator();
            calc.Visit(_expression);
            Assert.That(calc.Result, Is.EqualTo(6));
        }

    }

}
