/*
 * Keep a memento of an object's state to return to that state.
 *
 * A token/handle representing the system state. Lets us roll back to the state
 * when the token was generated. May or may not directly expose state information.
 *
 * This is different to the Command pattern where every change is recorded and each
 * command is able to 'undo' itself. We can maintain a list of memento's as per below
 * and use this with a position marker to provide undo and redo functionality.
 *
 */

using System.Collections.Generic;

namespace DesignPatterns.Patterns.Memento {

    public class Memento {
        public int Balance { get; }

        public Memento(int balance) {
            Balance = balance;
        }
    }

    public class BankAccount {

        private int _balance;
        private readonly List<Memento> _changes = new List<Memento>();
        private int _current;

        public BankAccount(int balance) {
            _balance = balance;
            _changes.Add(new Memento(balance));
        }

        public Memento Deposit(int amount) {
            _balance += amount;
            var m = new Memento(_balance);
            _changes.Add(m);
            ++_current;
            return m;
        }

        public void Restore(Memento m) {
            if (m == null) return;
            _balance = m.Balance;
            _changes.Add(m);
            _current = _changes.Count - 1;
        }

        public Memento Undo() {
            if (_current <= 0) return null;
            var m = _changes[--_current];
            _balance = m.Balance;
            return m;
        }

        public Memento Redo() {
            if (_current + 1 >= _changes.Count) return null;
            var m = _changes[++_current];
            _balance = m.Balance;
            return m;
        }

        public override string ToString() {
            return $"{_balance}";
        }

    }

}