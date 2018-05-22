using System.Linq;
using DesignPatterns.Patterns.Adapter;
using NUnit.Framework;
using Square = DesignPatterns.SOLID.Solutions.Square;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Adapter Design Pattern
    /// </summary>
    [TestFixture]
    internal class AdapterTests {

        private Line _line;

        /// <summary>
        /// Initialisation for unit tests
        /// </summary>
        [SetUp]
        public void SetUp() {
            _line = new Line(new Point(1, 1), new Point(1, 2));
        }

        /// <summary>
        /// Convert square to a rectangle
        /// </summary>
        [Test]
        public void SquareToRectangleAdapter_ShouldCalculateAreaFromSquare() {
            var rectangle = new SquareToRectangleAdapter(new Square(3));
            Assert.That(rectangle.Area, Is.EqualTo(9));
        }

        /// <summary>
        /// Line Adapter Converts Line to Points
        /// </summary>
        [Test]
        public void LineToPointAdapter_ShouldConvertLineToPoints() {
            var points = new LineToPointAdapter(_line);
            Assert.That(points.Count(), Is.EqualTo(2));

            var point1 = points.ElementAt(0);
            var point2 = points.ElementAt(1);
            Assert.That(point1.X, Is.EqualTo(1));
            Assert.That(point1.Y, Is.EqualTo(1));
            Assert.That(point2.X, Is.EqualTo(1));
            Assert.That(point2.Y, Is.EqualTo(2));
        }

        /// <summary>
        /// Line Adapter Hashes Converted Lines
        /// </summary>
        [Test]
        public void LineToPointAdapter_ShouldHashSameLineValues() {
            var points1 = new LineToPointAdapter(_line);
            var points2 = new LineToPointAdapter(_line);
            Assert.That(points1.ElementAt(0), Is.EqualTo(points2.ElementAt(0)));
            Assert.That(points1.ElementAt(1), Is.EqualTo(points2.ElementAt(1)));
        }

    }

}
