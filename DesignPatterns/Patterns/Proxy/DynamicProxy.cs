/*
 * Example of how to generate a dynamic logging proxy using ImpromptuInterface (nuget)
 *
 */

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using ImpromptuInterface;

namespace DesignPatterns.Patterns.Proxy {

    public interface IBankAccount {
        void Deposit(int amount);
        bool Withdraw(int amount);
        string ToString();
    }

    public class BankAccount : IBankAccount {
        private int _balance;
        private int _overdraftLimit = -500;

        public void Deposit(int amount) {
            _balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {_balance}");
        }

        public bool Withdraw(int amount) {
            if (_balance - amount < _overdraftLimit) return false;            
            _balance -= amount;
            Console.WriteLine($"Withdrew ${amount}, balance is now {_balance}");
            return true;
        }

        public override string ToString() {
            return $"{nameof(_balance)}: {_balance}";
        }
    }

    public class Log<T> : DynamicObject where T : class, new() {

        private readonly T _subject;
        private readonly Dictionary<string, int> _methodCallCount = new Dictionary<string, int>();

        protected Log(T subject) {
            _subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }

        /// <summary>
        /// Factory method
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static TI As<TI>(T subject) where TI : class {
            if (!typeof(TI).IsInterface)
                throw new ArgumentException("TI must be an interface type");

            // Duck typing here!
            return new Log<T>(subject).ActLike<TI>();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result) {

            try {
                // Log the call to the method
                Console.WriteLine($"Invoking {_subject.GetType().Name}.{binder.Name} with arguments [{string.Join(",", args)}]");
                if (_methodCallCount.ContainsKey(binder.Name)) _methodCallCount[binder.Name]++;
                else _methodCallCount.Add(binder.Name, 1);

                // Delegate the call to the actual interface method
                result = _subject.GetType().GetMethod(binder.Name).Invoke(_subject, args);
                return true;
            }
            catch {
                result = null;
                return false;
            }

        }

        public string Info {
            get {
                var sb = new StringBuilder();
                foreach (var kv in _methodCallCount)
                    sb.AppendLine($"{kv.Key} called {kv.Value} time(s)");
                return sb.ToString();
            }
        }

        // This will not be proxied automatically
        public override string ToString() {
            return $"{Info}";
        }

    }

}
