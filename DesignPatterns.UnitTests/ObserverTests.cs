using DesignPatterns.Patterns.Observer;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Observer Design Pattern
    /// </summary>
    [TestFixture]
    internal class ObserverTests {

        private bool _doctorCalled;

        /// <summary>
        /// Initialisation for unit tests
        /// </summary>
        [SetUp]
        public void SetUp() {
            _doctorCalled = false;
        }

        /// <summary>
        /// Simple decorator implementation of sealed StringBuilder
        /// </summary>
        [Test]
        public void Person_ShouldCallDoctor_WhenFallsIll() {
            Assert.That(_doctorCalled, Is.EqualTo(false));
            var person = new Person();
            person.FallsIll += CallDoctor;
            person.CatchACold();
            Assert.That(_doctorCalled, Is.EqualTo(true));
        }

        /// <summary>
        /// Helper event handler 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void CallDoctor(object sender, FallsIllEventArgs eventArgs) {
            _doctorCalled = true;
        }

        /// <summary>
        /// Test for single rat created
        /// </summary>
        [Test]
        public void Game_SingleRatTest() {
            var game = new Game();
            var rat = new Rat(game);
            Assert.That(rat.Attack, Is.EqualTo(1));
        }

        /// <summary>
        /// Test for two rats created
        /// </summary>
        [Test]
        public void Game_TwoRatTest() {
            var game = new Game();
            var rat = new Rat(game);
            var rat2 = new Rat(game);
            Assert.That(rat.Attack, Is.EqualTo(2));
            Assert.That(rat2.Attack, Is.EqualTo(2));
        }

        /// <summary>
        /// Test for three rats created with one dieing
        /// </summary>
        [Test]
        public void Game_ThreeRatsOneDies() {

            var game = new Game();
            var rat = new Rat(game);
            Assert.That(rat.Attack, Is.EqualTo(1));

            var rat2 = new Rat(game);
            Assert.That(rat.Attack, Is.EqualTo(2));
            Assert.That(rat2.Attack, Is.EqualTo(2));

            using (var rat3 = new Rat(game)) {
                Assert.That(rat.Attack, Is.EqualTo(3));
                Assert.That(rat2.Attack, Is.EqualTo(3));
                Assert.That(rat3.Attack, Is.EqualTo(3));
            }

            Assert.That(rat.Attack, Is.EqualTo(2));
            Assert.That(rat2.Attack, Is.EqualTo(2));
        }

    }

}
