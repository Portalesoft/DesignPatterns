/*
 *
 */

using System;
using System.Collections.Generic;

namespace DesignPatterns.Patterns.Factories {

    public interface IHotDrink {
        void Consume();
    }

    internal class Tea : IHotDrink {
        public void Consume() {
            Console.WriteLine("More tea please!");
        }
    }

    internal class Coffee : IHotDrink {
        public void Consume() {
            Console.WriteLine("I preferred the tea!");
        }
    }

    public interface IHotDrinkFactory {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory {
        public IHotDrink Prepare(int amount) {
            Console.WriteLine($"Preparing {amount} cups of tea ...");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory {
        public IHotDrink Prepare(int amount) {
            Console.WriteLine($"Preparing {amount} cups of coffee ...");
            return new Coffee();
        }
    }

    public class HotDrinkMachine {

        // The correct implementation would use a dependency injection container
        // rather than reflection but for simplicity using reflection.

        private readonly Dictionary<string, IHotDrinkFactory> _factories =
            new Dictionary<string, IHotDrinkFactory>();

        public HotDrinkMachine() {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes()) {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface) {
                    _factories.Add(
                        t.Name.Replace("Factory", string.Empty), 
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                    );
                }
            }
        }

        public IHotDrink MakeDrink(string drink, int amount) {
            return _factories.ContainsKey(drink) ? _factories[drink].Prepare(amount) : null;
        }

        //
        // The example code below shows a common implementation pattern that breaks
        // the open close principle. The enum has to be extended in order to add additional 
        // drinks.
        //
        //    public enum AvailableDrink { Coffee, Tea }
        //
        //    private Dictionary<AvailableDrink, IHotDrinkFactory> _factories =
        //        new Dictionary<AvailableDrink, IHotDrinkFactory>();
        //
        //    public HotDrinkMachine() {
        //        foreach(AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink))) {
        //            var factory = (IHotDrinkFactory)Activator.CreateInstance(
        //                    Type.GetType("DesignPatterns.Patterns.Factories." + 
        //                    Enum.GetName(typeof(AvailableDrink), drink) + "Factory") 
        //                );
        //            _factories.Add(drink, factory);
        //        }
        //    }
        //
        //    public IHotDrink MakeDrink(AvailableDrink drink, int amount) {
        //        return _factories[drink].Prepare(amount);
        //    }

    }
}
