using DesignPatterns.Patterns.Proxy;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Proxy Design Pattern
    /// </summary>
    [TestFixture]
    internal class ProxyTests {

        /// <summary>
        /// Test the dynamic logging proxy 
        /// </summary>
        [Test]
        public void Log_ShouldLogBankAccountCalls() {
            var ba = Log<BankAccount>.As<IBankAccount>(new BankAccount());
            ba.Deposit(50);
            ba.Deposit(70);
            Assert.That(ba.ToString(), Is.EqualTo("Deposit called 2 time(s)\r\n"));
        }

    }

}
