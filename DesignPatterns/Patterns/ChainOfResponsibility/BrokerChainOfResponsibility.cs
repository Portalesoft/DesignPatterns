/*
 * Using a mediator pattern we can implement a broker chain of responsibility
 * without directly modifying our creature object.
 *
 */

using System;

namespace DesignPatterns.Patterns.ChainOfResponsibility {

    public class Query {

        public string CreatureName;
        public enum Argument {
            Attack, Defense
        }

        public Argument WhatToQuery;
        public int Value; 
        public Query(string creatureName, Argument whatToQuery, int value) {
            CreatureName = creatureName ?? throw new ArgumentNullException(paramName: nameof(creatureName));
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    /// <summary>
    /// Mediator design pattern
    /// </summary>
    public class Game {

        // Implement the chain using an event handler list
        public event EventHandler<Query> Queries; 
        public void PerformQuery(object sender, Query q) {
            Queries?.Invoke(sender, q);
        }

    }

    public class DungeonCreature {

        private readonly Game _game;
        public string Name;
        private readonly int _attack;
        private readonly int _defense;

        public DungeonCreature(Game game, string name, int attack, int defense) {
            _game = game ?? throw new ArgumentNullException(paramName: nameof(game));
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            _attack = attack;
            _defense = defense;
        }

        public int Attack {
            get {
                var q = new Query(Name, Query.Argument.Attack, _attack);
                _game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public int Defense {
            get {
                var q = new Query(Name, Query.Argument.Defense, _defense);
                _game.PerformQuery(this, q);
                return q.Value;
            }
        }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(_attack)}: {Attack}, {nameof(_defense)}: {Defense}";
        }

    }

    /// <summary>
    /// Adding and removing to the chain is done using event registration
    /// </summary>
    public abstract class DungeonCreatureModifier : IDisposable {

        protected Game Game;
        protected DungeonCreature Creature;

        protected DungeonCreatureModifier(Game game, DungeonCreature creature) {
            Game = game;
            Creature = creature;
            game.Queries += Handle;
        }

        protected abstract void Handle(object sender, Query q);

        public void Dispose() {
            Game.Queries -= Handle;
        }

    }

    public class DoubleAttackCreatureModifier : DungeonCreatureModifier {

        public DoubleAttackCreatureModifier(Game game, DungeonCreature creature) : base(game, creature) {}
        protected override void Handle(object sender, Query q) {
            if (q.CreatureName == Creature.Name &&
                q.WhatToQuery == Query.Argument.Attack)
                q.Value *= 2;
        }

    }

    public class IncreaseDefenseCreatureModifier : DungeonCreatureModifier {

        public IncreaseDefenseCreatureModifier(Game game, DungeonCreature creature) : base(game, creature) {}
        protected override void Handle(object sender, Query q) {
            if (q.CreatureName == Creature.Name &&
                q.WhatToQuery == Query.Argument.Defense)
                q.Value += 2;
        }

    }

}
