/*
 * High level parts of the system should not depend on low level parts of the system directly
 * but instead they should depend on some sort of abstraction.
 *
 */

using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace DesignPatterns.SOLID.Solutions {

    public enum Relationship {
        Parent,
        Child,
        Sibling
    }

    public class Person {
        public string Name;
    }

    public interface IRelationshipBrowser {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class Relationships: IRelationshipBrowser {

        private readonly List<(Person, Relationship, Person)> _relations
            = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child) {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name) {
            return _relations
                .Where(x => x.Item1.Name == name
                            && x.Item2 == Relationship.Parent).Select(r => r.Item3);
        }
    }

    public class Research {

        public Research(IRelationshipBrowser browser) {
            foreach (var p in browser.FindAllChildrenOf("John")) {
                WriteLine($"John has a child called {p.Name}");
            }
        }

    }

}