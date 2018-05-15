/*
 * This example uses recursive generics to show inheritance of builders in a
 * complex scenario, this allows us to support the open close principle
 */

namespace DesignPatterns.Patterns.Builder {

    public class Person {

        public string Name { get; set; }
        public string Position { get; set; }

        public class Builder : PersonJobBuilder<Builder> { }
        public static Builder New => new Builder();

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }

    }

    public abstract class PersonBuilder {
        protected Person Person = new Person();
        public Person Build() {
            return Person;
        }
    }

    public class PersonInfoBuilder<TSelf>: PersonBuilder 
        where TSelf : PersonInfoBuilder<TSelf> {

        public TSelf Called(string name) {
            Person.Name = name;
            return (TSelf)this;
        }

    }

    public class PersonJobBuilder<TSelf> : PersonInfoBuilder<PersonJobBuilder<TSelf>> 
        where TSelf : PersonJobBuilder<TSelf> {

        public TSelf WorksAsA(string position) {
            Person.Position = position;
            return (TSelf)this;
        }

    }

}
