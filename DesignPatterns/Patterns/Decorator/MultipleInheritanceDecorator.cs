/*
 * There's no multiple inheritance in C#. Using decorators it's possible to implement something akin
 * to this nb: the example below could also be implemented using an IBird and ILizard interface
 * and pulling the methods up to those interfaces. If the interface shared a common property or method
 * this can be navigated by implementing the actual public property or method direct on the class
 * without needing an explicit implementation of either IBird or ILizard!
 *
 */

using System;

namespace DesignPatterns.Patterns.Decorator {

    public class Bird {
        public void Fly() { }
    }

    public class Lizard {
        public void Crawl() { }
    }

    public class Dragon {

        private readonly Bird _bird;
        private readonly Lizard _lizard;

        public Dragon(Bird bird, Lizard lizard) {
            _bird = bird ?? throw new ArgumentNullException(nameof(bird));
            _lizard = lizard ?? throw new ArgumentNullException(nameof(lizard));
        }

        public void Crawl() {
            _lizard.Crawl();
        }

        public void Fly() {
            _bird.Fly();
        }
    }

}