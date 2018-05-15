/*
 * Exposing several components through a single interface. Balances complexity with presentation and usability.
 *
 * Provides a simple, easy to understand/user interface over a large and sophisticated body of code.

 * Build a Facade to provide a simplified API over a set of classes.
 *
 * May wish to optionally expose internals through the facade
 *
 */

using System;

namespace DesignPatterns.Patterns.Facade {

    internal class SubSystemOne {
        public void MethodOne() {
            Console.WriteLine(" SubSystemOne Method");
        }
    }

    internal class SubSystemTwo {
        public void MethodTwo() {
            Console.WriteLine(" SubSystemTwo Method");
        }
    }

    internal class SubSystemThree {
        public void MethodThree() {
            Console.WriteLine(" SubSystemThree Method");
        }
    }

    internal class SubSystemFour {
        public void MethodFour() {
            Console.WriteLine(" SubSystemFour Method");
        }
    }

    /// <summary>
    /// The 'Facade' class
    /// </summary>
    public class Facade {

        private readonly SubSystemOne _one;
        private readonly SubSystemTwo _two;
        private readonly SubSystemThree _three;
        private readonly SubSystemFour _four;

        public Facade() {
            _one = new SubSystemOne();
            _two = new SubSystemTwo();
            _three = new SubSystemThree();
            _four = new SubSystemFour();
        }

        public void MethodA() {
            Console.WriteLine("\nMethodA() ---- ");
            _one.MethodOne();
            _two.MethodTwo();
            _four.MethodFour();
        }

        public void MethodB() {
            Console.WriteLine("\nMethodB() ---- ");
            _two.MethodTwo();
            _three.MethodThree();
        }
    }

}