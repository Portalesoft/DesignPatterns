/*
 * A no-op object that conforms to the required interface, satisfying a dependency
 * requirement of some other object. A behavioural design pattern with no behaviours!
 *
 * When component A uses component B, it typically assumes that B is not null:
 *
 *  Typically you inject B, not B? or some Option<B>
 *  You do not check for null (?.) on every call, this should only be used for events
 *  
 * There is no option of telling A not to use an instance of B, its use is hard coded
 *
 * Thus we build a no-op, non functioning inheritor of B and pass it into A. If the methods
 * are non-void, return default(T).
 *
 * An additional example using the DLR to create a generic null object is shown below
 * for informational purposes, this will have a performance overhead!
 *
 */

using System;
using System.Dynamic;
using ImpromptuInterface;
using static System.Console;

namespace DesignPatterns.Patterns.NullObject {

    public interface ILog {
        void Info(string msg);
        void Warn(string msg);
    }

    class ConsoleLog : ILog {
        public void Info(string msg) {
            WriteLine(msg);
        }

        public void Warn(string msg) {
            WriteLine("WARNING: " + msg);
        }
    }

    public class BankAccount {
        private ILog log;
        private int balance;

        public BankAccount(ILog log) {
            this.log = log;
        }

        public void Deposit(int amount) {
            balance += amount;
            // One option is to check for null everywhere
            log?.Info($"Deposited ${amount}, balance is now {balance}");
        }

        public void Withdraw(int amount) {
            if (balance >= amount) {
                balance -= amount;
                log?.Info($"Withdrew ${amount}, we have ${balance} left");
            } else {
                log?.Warn($"Could not withdraw ${amount} because balance is only ${balance}");
            }
        }
    }

    /// <summary>
    /// Another alternative is to use a log implementation which does nothing
    /// </summary>
    public sealed class NullLog : ILog {
        public void Info(string msg) { }
        public void Warn(string msg) {}
    }

    /// <summary>
    /// Or another version using the DLR E.g.: var log = Null{ILog}.Instance;
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Null<T> : DynamicObject where T : class {
        public static T Instance {
            get {
                if (!typeof(T).IsInterface)
                    throw new ArgumentException("I must be an interface type");
                return new Null<T>().ActLike<T>();
            }
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result) {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }

        private class Empty { }
    }

}