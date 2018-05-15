using System;
using System.Collections.Generic;
using System.Linq;
using DesignPatterns.Patterns.Flyweight;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Flyweight Design Pattern
    /// </summary>
    [TestFixture]
    internal class FlyweightTests {

        private IEnumerable<string> _firstNames;
        private IEnumerable<string> _lastNames;

        /// <summary>
        /// Initialisation for unit tests
        /// </summary>
        [SetUp]
        public void SetUp() {
            _firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            _lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
        }

        /// <summary>
        /// Test the memory allocation for the user class under dotmemory 
        /// </summary>
        [Test]
        [method: DotMemoryUnit(FailIfRunWithoutSupport = false)]
        public void User_TestMemoryAllocation() {

            var users = new List<User>();
            foreach (var firstName in _firstNames)
            foreach (var lastName in _lastNames) {
                users.Add(new User($"{firstName} {lastName}"));
            }

            ForceGc();
            dotMemory.Check(memory => {
                Console.WriteLine(memory.SizeInBytes);
            });

        }

        /// <summary>
        /// Test the memory allocation for the flyweight user class under dotmemory 
        /// </summary>
        [Test]
        [method: DotMemoryUnit(FailIfRunWithoutSupport = false)]
        public void FlyweightUser_TestMemoryAllocation() {

            var users = new List<FlyweightUser>();
            foreach (var firstName in _firstNames)
            foreach (var lastName in _lastNames) {
                users.Add(new FlyweightUser($"{firstName} {lastName}"));
            }

            ForceGc();
            dotMemory.Check(memory => {
                Console.WriteLine(memory.SizeInBytes);
            });

        }

        /// <summary>
        /// Test that creating a flyweight users retains the same data
        /// </summary>
        [Test]
        public void FlyweightUser_ShouldCreateUser() {
            var user = new FlyweightUser("Barry Jacobs");
            Assert.That(user.FullName, Is.EqualTo("Barry Jacobs"));
        }

        /// <summary>
        /// Generate a random string
        /// </summary>
        /// <returns></returns>
        private static string RandomString() {
            var rand = new Random();
            return new string(
                Enumerable.Range(0, 10)
                    .Select(i => (char)('a' + rand.Next(26)))
                    .ToArray());
        }

        /// <summary>
        /// Force garbage collection
        /// </summary>
        private static void ForceGc() {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

    }

}
