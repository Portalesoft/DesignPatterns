using System;
using DesignPatterns.Patterns.ChainOfResponsibility;
using DesignPatterns.Patterns.Proxy;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Chain of Responsibility Design Pattern
    /// </summary>
    [TestFixture]
    internal class ChainOfResponsibilityTests {

        /// <summary>
        /// Test the method chain of responsibility example
        /// </summary>
        [Test]
        public void Goblin_ShouldApplyMethodModificationChain() {

            var goblin = new Creature("Goblin", 2, 2);
            Assert.That(goblin.Attack, Is.EqualTo(2));
            Assert.That(goblin.Defense, Is.EqualTo(2));

            var root = new CreatureModifier(goblin);
            root.Add(new DoubleAttackModifier(goblin));
            root.Add(new IncreaseDefenseModifier(goblin));
            root.Handle();
            Assert.That(goblin.Attack, Is.EqualTo(4));
            Assert.That(goblin.Defense, Is.EqualTo(5));

        }

        /// <summary>
        /// Test the broker chain of responsibility example
        /// </summary>
        [Test]
        public void Goblin_ShouldApplyBrokerModificationChain() {

            var game = new Game();
            var goblin = new DungeonCreature(game, "Strong Goblin", 3, 3);
            Assert.That(goblin.Attack, Is.EqualTo(3));
            Assert.That(goblin.Defense, Is.EqualTo(3));

            using (new DoubleAttackCreatureModifier(game, goblin)) {

                Assert.That(goblin.Attack, Is.EqualTo(6));
                Assert.That(goblin.Defense, Is.EqualTo(3));

                using (new IncreaseDefenseCreatureModifier(game, goblin)) {
                    Assert.That(goblin.Attack, Is.EqualTo(6));
                    Assert.That(goblin.Defense, Is.EqualTo(5));
                }

            }

            // Both modifiers disposed
            Assert.That(goblin.Attack, Is.EqualTo(3));
            Assert.That(goblin.Defense, Is.EqualTo(3));

        }

    }

}
