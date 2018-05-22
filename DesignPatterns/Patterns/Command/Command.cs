/*
 * An object which represents an instruction to perform a particular action.
 * Contains all the information necessary for the action to be taken.
 *
 * Ordinary C# statements are perishable i.e.: no undo functionality, can't serialise
 * a sequence of actions.
 *
 * Encapsulate all details of an operation in a separate object which allows, for example, serialisation.
 *
 * Commonly used in GUI commands, undo/redo and macro recording.
 *
 */

using System;

namespace DesignPatterns.Patterns.Command {

    public class BankAccount {
        private int _balance;
        private const int OverdraftLimit = -500;

        internal void Deposit(int amount) {
            _balance += amount;
        }

        internal bool Withdraw(int amount) {
            if (_balance - amount < OverdraftLimit) return false;
            _balance -= amount;
            return true;
        }

        public override string ToString() {
            return $"{_balance}";
        }
    }

    public interface ICommand {
        void Call();
        void Undo();
    }

    public class BankAccountCommand : ICommand {

        private readonly BankAccount _account;

        public enum Action {
            Deposit,
            Withdraw
        }

        private readonly Action _action;
        private readonly int _amount;
        private bool _succeeded;

        public BankAccountCommand(BankAccount account, Action action, int amount) {
            _account = account ?? throw new ArgumentNullException(paramName: nameof(account));
            _action = action;
            _amount = amount;
        }

        public void Call() {
            switch (_action) {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    _succeeded = true;
                    break;
                case Action.Withdraw:
                    _succeeded = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo() {
            if (!_succeeded) return;
            switch (_action) {
                case Action.Deposit:
                    _account.Withdraw(_amount);
                    break;
                case Action.Withdraw:
                    _account.Deposit(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}