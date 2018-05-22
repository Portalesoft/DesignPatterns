using System;
using System.Collections.Generic;
using System.IO;

namespace DesignPatterns.SOLID.Problems {

    public class Journal {

        private readonly List<string> _entries = new List<string>();
        private static int _count;

        public int AddEntry(string text) {
            _entries.Add($"{++_count}: {text}");
            return _count; // Memento pattern!
        }

        public void RemoveEntry(int index) {
            _entries.RemoveAt(index);
        }

        public override string ToString() {
            return string.Join(Environment.NewLine, _entries);
        }

        // Breaks single responsibility principle
        public void Save(string filename, bool overwrite = false) {
            File.WriteAllText(filename, ToString());
        }

    }

}