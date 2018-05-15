/*
 * Simple game where one or more rats attack a player. Each rat has an attack value of 1.
 * Rats attack as a swarm, so each rat's attack value is equal to the number of rats in play.
 *
 * A rat enters play through its constructor and leaves (dies) via its Dispose method.
 *
 */

using System;

namespace DesignPatterns.Patterns.Observer
{
    public class Game {

        public event EventHandler RatEnters, RatDies;
        public event EventHandler<Rat> NotifyRat;

        public void FireRatEnters(object sender) {
            RatEnters?.Invoke(sender, EventArgs.Empty);
        }

        public void FireRatDies(object sender) {
            RatDies?.Invoke(sender, EventArgs.Empty);
        }

        public void FireNotifyRat(object sender, Rat whichRat) {
            NotifyRat?.Invoke(sender, whichRat);
        }

    }

    public class Rat : IDisposable {

        private readonly Game _game;
        public int Attack = 1;

        public Rat(Game game) {
            _game = game;
            game.RatEnters += (sender, args) => {
                if (sender == this) return;
                ++Attack;
                game.FireNotifyRat(this, (Rat)sender);
            };
            game.NotifyRat += (sender, rat) => {
                if (rat == this) ++Attack;
            };
            game.RatDies += (sender, args) => --Attack;
            game.FireRatEnters(this);
        }

        public void Dispose() {
            _game.FireRatDies(this);
        }

    }

}
