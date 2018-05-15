using System.Collections.Generic;
using System.Linq;
using DesignPatterns.Patterns.Command;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Command Design Pattern
    /// </summary>
    [TestFixture]
    internal class CommandTests {

        private BankAccount _ba;
        private List<BankAccountCommand> _commands;

        /// <summary>
        /// Initialisation for unit tests
        /// </summary>
        [SetUp]
        public void SetUp() {
            _ba = new BankAccount();
            _commands = new List<BankAccountCommand> {
                new BankAccountCommand(_ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(_ba, BankAccountCommand.Action.Withdraw, 50)
            };
        }

        /// <summary>
        /// Test that we can apply our command sequence including undo
        /// </summary>
        [Test]
        public void BankAccount_ShouldApplyCommands() {
            Assert.That(_ba.ToString(), Is.EqualTo("0"));
            foreach (var c in _commands)
                c.Call();
            Assert.That(_ba.ToString(), Is.EqualTo("50"));
            foreach (var c in _commands)
                c.Undo();
            Assert.That(_ba.ToString(), Is.EqualTo("0"));
        }

    }

}
