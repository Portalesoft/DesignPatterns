using DesignPatterns.Patterns.Memento;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Memento Design Pattern
    /// </summary>
    [TestFixture]
    internal class MementoTests {

        private BankAccount _ba;

        /// <summary>
        /// Initialisation for unit tests
        /// </summary>
        [SetUp]
        public void SetUp() {
            _ba = new BankAccount(100);
        }

        /// <summary>
        /// Restore the balance from a memento
        /// </summary>
        [Test]
        public void BankAccount_ShouldRestoreMementoBalance() {

            var m1 = _ba.Deposit(50); // 150
            var m2 = _ba.Deposit(25); // 175
            Assert.That(_ba.ToString(), Is.EqualTo("175"));

            _ba.Restore(m1);
            Assert.That(_ba.ToString(), Is.EqualTo("150"));

        }

        /// <summary>
        /// Test the undo and redo functionality
        /// </summary>
        [Test]
        public void BankAccount_ShouldUndoAndRedoBalance() {

            _ba.Deposit(50); // 150
            _ba.Deposit(25); // 175
            Assert.That(_ba.ToString(), Is.EqualTo("175"));

            _ba.Undo();
            Assert.That(_ba.ToString(), Is.EqualTo("150"));

            _ba.Redo();
            Assert.That(_ba.ToString(), Is.EqualTo("175"));

        }

    }

}
