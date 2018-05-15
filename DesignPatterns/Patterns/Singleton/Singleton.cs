/*
 * A component which is instantiated only once, for some components it only makes 
 * sense to have one in the system e.g.:
 * 
 *      Database Repository
 *      Object Factory
 *      
 * Also, where the constructor call is expensive:
 * 
 *      We do it only once
 *      We provide everyone with the same instance
 *      
 * Additionally:    
 *     
 *      Need to prevent anyone creating additional copies
 *      Need to take care of lazy instantiation and thread safety
 *      
 * Note: Singletons can be difficult to test and in best practice should be defined
 * within a dependency injection container, if this is being used.
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MoreLinq;

namespace DesignPatterns.Patterns.Singleton {

    public interface IDatabase {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase {

        private readonly Dictionary<string, int> _capitals;

        public static SingletonDatabase Instance => LazyInstance.Value;
        private static readonly Lazy<SingletonDatabase> LazyInstance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        private SingletonDatabase() {
            // Increment count for unit tests
            Count++;

            // Batch is from the MoreLinq package
            var assembly = Assembly.GetExecutingAssembly();
            var reader = new StreamReader(assembly.GetManifestResourceStream("DesignPatterns.Patterns.Singleton.Capitals.txt"));
            _capitals = reader.ReadToEnd()
                .Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1)));
        }

        public static int Count { get; private set; }

        public int GetPopulation(string name) {
            return _capitals[name];
        }
    }

    public class SingletonRecordFinder {
        public int TotalPopulation(IEnumerable<string> names) {
            return names.Sum(name => SingletonDatabase.Instance.GetPopulation(name));
        }
    }

    public class ConfigurableRecordFinder {
        private readonly IDatabase _database;

        public ConfigurableRecordFinder(IDatabase database) {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public int GetTotalPopulation(IEnumerable<string> names) {
            return names.Sum(name => _database.GetPopulation(name));
        }
    }

    public class DummyDatabase : IDatabase {
        public int GetPopulation(string name) {
            return new Dictionary<string, int> {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }

}