using System.Text;
using DesignPatterns.Patterns.Builder;
using NUnit.Framework;

namespace DesignPatterns.UnitTests {

    /// <summary>
    /// Unit tests for the Builder Design Pattern
    /// </summary>
    [TestFixture]
    internal class BuilderTests {

        /// <summary>
        /// Simple builder test
        /// </summary>
        [Test]
        public void HtmlBuilder_ShouldReturnTwoLineItems() {
            var expectedOuput = new StringBuilder();
            expectedOuput.AppendLine("<ul>");
            expectedOuput.AppendLine($"{"".PadLeft(2)}<li>");
            expectedOuput.AppendLine($"{"".PadLeft(4)}Hello");
            expectedOuput.AppendLine($"{"".PadLeft(2)}</li>");
            expectedOuput.AppendLine($"{"".PadLeft(2)}<li>");
            expectedOuput.AppendLine($"{"".PadLeft(4)}World");
            expectedOuput.AppendLine($"{"".PadLeft(2)}</li>");
            expectedOuput.AppendLine("</ul>");

            var builder = new HtmlBuilder("ul")
                .AddChild("li", "Hello")
                .AddChild("li", "World");
            Assert.That(builder.ToString(), Is.EqualTo(expectedOuput.ToString()));
        }

        /// <summary>
        /// Fluent Builder Test
        /// </summary>
        [Test]
        public void TeamBuilder_ShouldReturnTeam() {
            var tb = new TeamBuilder();
            Team team = tb.CreateTeam("Norwich")
                .WithNickName("The Canaries")
                .FromTown("Norwich")
                .PlayingAt("Carrow Road");

            Assert.That(team.Name, Is.EqualTo("Norwich"));
            Assert.That(team.NickName, Is.EqualTo("The Canaries"));
            Assert.That(team.HomeTown, Is.EqualTo("Norwich"));
            Assert.That(team.Ground, Is.EqualTo("Carrow Road"));
        }

        /// <summary>
        /// Fluent Builder with Inheritance Test
        /// </summary>
        [Test]
        public void PersonBuilder_ShouldReturnPerson() {
            var person = Person.New
                .Called("Barry")
                .WorksAsA("Developer")
                .Build();

            Assert.That(person.GetType(), Is.EqualTo(typeof(Person)));
            Assert.That(person.Name, Is.EqualTo("Barry"));
            Assert.That(person.Position, Is.EqualTo("Developer"));
        }

        /// <summary>
        /// Faceted Builder Test
        /// </summary>
        [Test]
        public void EmployeeBuilder_ShouldReturnEmployee() {
            var builder = new EmployeeBuilder();
            Employee employee = builder
                .Works.In("Development")
                      .AsA("Team Leader")
                .Lives.At("24 The Street")
                      .In("Basingstoke")
                      .WithPostcode("RG21 4FB");

            Assert.That(employee.Department, Is.EqualTo("Development"));
            Assert.That(employee.Position, Is.EqualTo("Team Leader"));
            Assert.That(employee.StreetAddress, Is.EqualTo("24 The Street"));
            Assert.That(employee.City, Is.EqualTo("Basingstoke"));
            Assert.That(employee.PostCode, Is.EqualTo("RG21 4FB"));
        }
    }    
}