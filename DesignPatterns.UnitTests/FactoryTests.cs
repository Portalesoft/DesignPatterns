using DesignPatterns.Patterns.Factories;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Factory Design Pattern
    /// </summary>
    [TestFixture]
    internal class FactoryTests {

        // Private instance of our drinks machine
        private HotDrinkMachine _machine;

        /// <summary>
        /// Initialisation for unit tests
        /// </summary>
        [SetUp]
        public void SetUp() {
            _machine = new HotDrinkMachine();
        }

        /// <summary>
        /// Simple Factory Method Test
        /// </summary>
        [Test]
        public void Point_IsCartesianPoint() {
            var point = Point.NewCartesianPoint(10, 20);
            Assert.That(point.ToString(), Is.EqualTo("_x: 10, _y: 20"));
        }

        /// <summary>
        /// Inner Factory Method Test
        /// </summary>
        [Test]
        public void PointFactory_CanCreateCartesianPoint() {
            var point = Point.Factory.NewInnerCartesianPoint(10, 20);
            Assert.That(point.ToString(), Is.EqualTo("_x: 10, _y: 20"));
        }

        /// <summary>
        /// Abstract Factory Method Success Tests
        /// </summary>
        /// <param name="drinkType">Type of drink to make</param>
        [Test]
        [TestCase("Coffee")]
        [TestCase("Tea")]
        public void HotDrinkMachine_ShouldMakeAvailableDrink(string drinkType) {
            var drink = _machine.MakeDrink(drinkType, 1);
            Assert.That(drink, Is.Not.Null);
            Assert.That(drink.GetType().Name, Is.EqualTo(drinkType));
        }

        /// <summary>
        /// Abstract Factory Method Failure Test
        /// </summary>
        /// <param name="drinkType">Type of drink to make</param>
        [Test]
        [TestCase("Hot Chocolate")]
        public void HotDrinkMachine_ShouldNotMakeUnavailableDrink(string drinkType) {
            var drink = _machine.MakeDrink(drinkType, 1);
            Assert.That(drink, Is.Null);
        }
    }
}