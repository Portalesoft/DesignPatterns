/*
 * High level parts of the system should not depend on low level parts of the system directly
 * but instead they should depend on some sort of abstraction.
 *
 */

using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace DesignPatterns.SOLID.Problems {

    public enum Relationship {
        Parent,
        Child,
        Sibling
    }

    public class Person {
        public string Name;
    }

    public class Relationships {

        public void AddParentAndChild(Person parent, Person child) {
            Relations.Add((parent, Relationship.Parent, child));
            Relations.Add((child, Relationship.Child, parent));
        }

        public List<(Person, Relationship, Person)> Relations { get; } = new List<(Person, Relationship, Person)>();

    }

    public class Research {

        /// <summary>
        /// High level access to the low level data store of the relationships
        /// This means that relationships can't change it's mind on how to store its
        /// internal data
        /// </summary>
        /// <param name="relationships"></param>
        public Research(Relationships relationships) {
            var relations = relationships.Relations;
            foreach (var r in relations
                .Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent)) {
                    WriteLine($"John has a child called {r.Item3.Name}");
            }
        }

    }

}