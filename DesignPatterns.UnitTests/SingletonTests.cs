// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingletonTests.cs" company="Portalesoft">
//   Copyright (c) Portalesoft Ltd. All rights reserved.
// </copyright>
// <summary>
//   Unit tests for the Singleton Design Pattern
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using DesignPatterns.Patterns.Singleton;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Singleton Design Pattern
    /// </summary>
    [TestFixture]
    public class SingletonTests {

        /// <summary>
        /// Singleton Creation Test
        /// </summary>
        [Test]
        public void SingletonDatabase_ShouldReturnSameInstance() {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Simulate Access to Live Singleton Database Test
        /// </summary>
        [Test]
        public void SingletonRecordFinder_ShouldCalculateTotalPopulation() {
            var rf = new SingletonRecordFinder();
            var names = new[] { "Seoul", "Mexico City" };
            var tp = rf.TotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 17400000));
        }

        /// <summary>
        /// Configure Access to Simulated Database Test
        /// </summary>
        [Test]
        public void ConfigurableRecordFinder_ShouldCalculateTotalPopulation() {
            var db = new DummyDatabase();
            var rf = new ConfigurableRecordFinder(db);
            Assert.That(
              rf.GetTotalPopulation(new[] { "alpha", "gamma" }),
              Is.EqualTo(4));
        }

        /// <summary>
        /// Monostate Singleton Creation Test
        /// </summary>
        [Test]
        public void ChiefExecutiveOfficer_ShouldReturnSameInstanceData() {
            var ceo1 = new ChiefExecutiveOfficer {
                Name = "Barry Jacobs",
                Age = 48
            };
            var ceo2 = new ChiefExecutiveOfficer();     
            Assert.That(ceo1.ToString(), Is.EqualTo(ceo2.ToString()));
        }
    }
}
