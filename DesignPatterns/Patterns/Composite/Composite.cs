/*
 * Treating individual (scalar) objects and composite objects uniformly
 *
 * In the example below in order to connect Neurons with Neuron Layers as well as layers to layers
 * and neurons to neurons, both need to provide the same interface, which is in this case is IEnumerable.
 *
 * The connection is then provided via an extension. 
 *
 */

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesignPatterns.Patterns.Composite {

    /// <inheritdoc />
    /// <summary>
    /// Masquerade as a collection via yield return this
    /// </summary>
    public class Neuron : IEnumerable<Neuron> {
        public float Value;
        public List<Neuron> In, Out;

        public IEnumerator<Neuron> GetEnumerator() {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            yield return this;
        }
    }

    public class NeuronLayer : Collection<Neuron> {}

    public static class ExtensionMethods {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other) {
            if (ReferenceEquals(self, other)) return;

            var enumerable = other.ToList();
            foreach (var from in self)
            foreach (var to in enumerable) {
                from.Out.Add(to);
                to.In.Add(from);
            }

        }
    }
}