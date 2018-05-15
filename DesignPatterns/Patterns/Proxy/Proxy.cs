/*
 * An interface for accessing a particular resource
 *
 * A class that functions as an interface to a particular resource.
 * That resource may be remote, expensive to construct or may require
 * logging or some other added functionality.
 *
 * Similar to the decorator pattern:
 *
 *  Proxy provides an identical interface; decorator provides an enhanced interface
 *  Decorator typically aggregates (takes in the ctor) what it's decorating, proxy doesn't need to
 *  Proxy might not even be working with a materialized object
 *
 */

using System;

namespace DesignPatterns.Patterns.Proxy {

    /// <summary>
    /// Example shown below is standard implementation of a protection proxy
    /// </summary>

    public interface ICar {
        void Drive();
    }

    public class Car : ICar {
        public void Drive() {
            Console.WriteLine("Car is being driven");
        }
    }

    public class CarProxy : ICar {

        private readonly Car _car = new Car();
        private readonly Driver _driver;

        public CarProxy(Driver driver) {
            _driver = driver;
        }

        public void Drive() {
            if (_driver.Age >= 16)
                _car.Drive();
            else {
                Console.WriteLine("Driver is too young!");
            }
        }

    }

    public class Driver {
        public int Age { get; set; }
        public Driver(int age) {
            Age = age;
        }
    }

}