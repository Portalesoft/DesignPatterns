﻿/*
 * Example of monostate singleton where public properties wrap static fields
 */

namespace DesignPatterns.Patterns.Singleton {

    public class ChiefExecutiveOfficer {
        private static string _name;
        private static int _age;

        public string Name {
            get => _name;
            set => _name = value;
        }

        public int Age {
            get => _age;
            set => _age = value;
        }

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }

}