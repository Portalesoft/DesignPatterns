/*
 * A space optimisation technique that lets us use less memory by storing externally the data
 * associated with similar objects.
 *
 * Avoid redundancy when storing data e.g.: mmorpg (millions of players)
 *
 *  Plenty of users with identical first/last names
 *  No sense in storing same first/last name over and over again
 *  Store a list of names and have pointers to them (indices or references)
 *
 * .NET performs string interning, so an identical string is stored only once (uses flyweight pattern)
 *
 * Can also be used to define the idea of ranges across data and store data related to the attributes of
 * the range, rather than modifiying the actual data itself.
 *
 */

using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Patterns.Flyweight {

    public class User {

        private string _fullName;
        public User(string fullName) {
            _fullName = fullName;
        }

    }

    public class FlyweightUser {

        private static readonly List<string> Strings = new List<string>();
        private readonly int[] _names;

        public FlyweightUser(string fullName) {

            int GetOrAdd(string s) {
                var idx = Strings.IndexOf(s);
                if (idx != -1) return idx;
                else {
                    Strings.Add(s);
                    return Strings.Count - 1;
                }
            }
            _names = fullName.Split(' ')
                .Select(GetOrAdd)
                .ToArray();

        }

        public string FullName => string.Join(" ", 
            _names.Select(i => Strings[i]).ToArray());

    }
}