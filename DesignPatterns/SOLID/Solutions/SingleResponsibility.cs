using System;
using System.Collections.Generic;
using System.IO;

namespace DesignPatterns.SOLID.Solutions {

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

    }

    // Handles the responsibility of persisting objects
    public class Persistence {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false) {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, journal.ToString());
        }
    }

}