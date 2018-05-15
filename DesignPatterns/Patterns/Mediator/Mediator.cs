/*
 * Facilitates communication between components.
 *
 * Components may go in and out of a system at any time.
 *
 * A mediator is a component that facilitates communication between other components
 * without them necessarilt being aware of each other or having a direct (reference)
 * access to each other.
 *
 * Create a mediator and have each object in the system refer to it e.g.: in a field and/or via ctor injection.
 * Mediator has functions components can call and components have functions mediator can call.
 *
 * Event processing e.g.: Rx libraries make communication easier to implement.
 *
 */

using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace DesignPatterns.Patterns.Mediator {

    public class Person {
        public string Name;
        public ChatRoom Room;
        private readonly List<string> _chatLog = new List<string>();

        public Person(string name) {
            Name = name;
        }

        public void Receive(string sender, string message) {
            var s = $"{sender}: '{message}'";
            WriteLine($"[{Name}'s chat session] {s}");
            _chatLog.Add(s);
        }

        public void Say(string message) {
            Room.Broadcast(Name, message);
        }

        public void PrivateMessage(string who, string message) {
            Room.Message(Name, who, message);
        }
    }

    public class ChatRoom {
        private readonly List<Person> _people = new List<Person>();

        public void Broadcast(string source, string message) {
            foreach (var p in _people)
                if (p.Name != source)
                    p.Receive(source, message);
        }

        public void Join(Person p) {
            var joinMsg = $"{p.Name} joins the chat";
            Broadcast("room", joinMsg);

            p.Room = this;
            _people.Add(p);
        }

        public void Message(string source, string destination, string message) {
            _people.FirstOrDefault(p => p.Name == destination)?.Receive(source, message);
        }
    }

}