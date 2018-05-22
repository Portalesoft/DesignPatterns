/*
 * A high level blueprint for an algorithm to be completed by inheritors. Allows us to
 * defined the 'skeleton' of the algorithm, with concrete implementations defined in
 * subclasses.
 *
 * Strategy pattern does this through composition:
 *
 *   High level algorithm uses an interface
 *   Concrete implementations implement the interface
 *
 * Template Method does the same thing through inheritance
 *
 *   Overall algorithm makes use of abstract member
 *   Inheritors override the abstract members
 *   Parent template method invoked
 *
 */

using static System.Console;

namespace DesignPatterns.Patterns.TemplateMethod {

    public abstract class Game {

        public void Run() {
            Start();
            while (!HaveWinner)
                TakeTurn();
            WriteLine($"Player {WinningPlayer} wins.");
        }

        protected abstract void Start();
        protected abstract bool HaveWinner { get; }
        protected abstract void TakeTurn();
        protected abstract int WinningPlayer { get; }

        protected int CurrentPlayer;
        protected readonly int NumberOfPlayers;

        protected Game(int numberOfPlayers) {
            NumberOfPlayers = numberOfPlayers;
        }

    }

    public class Chess : Game {

        private readonly int _maxTurns = 10;
        private int _turn = 1;

        public Chess() : base(2) { }

        protected override void Start() {
            WriteLine($"Starting a game of chess with {NumberOfPlayers} players.");
        }

        protected override bool HaveWinner => _turn == _maxTurns;

        protected override void TakeTurn() {
            WriteLine($"Turn {_turn++} taken by player {CurrentPlayer}.");
            CurrentPlayer = (CurrentPlayer + 1) % NumberOfPlayers;
        }

        protected override int WinningPlayer => CurrentPlayer;

    }

}