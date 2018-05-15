/*
 * Sequence of handlers processing an event one after another e.g.:
 *
 * Click a graphical element on a form, which raises an event:
 *
 *  Button handles event, stops further processing or ...
 *  Underlying group box or ...
 *  Underlying window
 *
 * Chain can be implemented as a chain of references or as a centralised construct.
 * Removal can be implemented via dispose.
 *
 * Command Query Separation:
 *
 *  Command = Asking for an action or change
 *  Query = Asking for information e.g.: give me the property value
 *  CQS = Having separate means of sending commands and queries to e.g.: direct field access
 *
 * Example below uses the method chain of responsibility pattern. This has issues though as we
 * modify our creature directly and there is no easy to remove a link in the chain as we would
 * need to execute the chain from the initial state, which we don't have! 
 *
 */

using System;
using static System.Console;

namespace DesignPatterns.Patterns.ChainOfResponsibility {

    public class Creature {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense) {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Attack = attack;
            Defense = defense;
        }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier {
        protected Creature Creature;
        protected CreatureModifier Next;

        public CreatureModifier(Creature creature) {
            Creature = creature ?? throw new ArgumentNullException(nameof(creature));
        }

        public void Add(CreatureModifier cm) {
            if (Next != null) Next.Add(cm);
            else Next = cm;
        }

        public virtual void Handle() => Next?.Handle();
    }

    public class NoBonusesModifier : CreatureModifier {
        public NoBonusesModifier(Creature creature) : base(creature) {}

        public override void Handle() {
            WriteLine("No bonuses for you!");
        }
    }

    public class DoubleAttackModifier : CreatureModifier {
        public DoubleAttackModifier(Creature creature) : base(creature) {}

        public override void Handle() {
            WriteLine($"Doubling {Creature.Name}'s attack");
            Creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier {
        public IncreaseDefenseModifier(Creature creature) : base(creature) {}

        public override void Handle() {
            WriteLine("Increasing goblin's defense");
            Creature.Defense += 3;
            base.Handle();
        }
    }

}