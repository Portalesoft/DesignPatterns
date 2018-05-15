/*
 * A prototype is a partially or fully initialised object that you copy (clone) and make use of. 
 * This requires deep copy support either by a manual deep copy method or interface.
 * 
 * The ICloneable implementation is generally a bad idea as there is no specificiation of whether the
 * clone is deep or shallow, in addition to the interface using objects.
 * 
 * Another implementation is using a C# copy constructor and copying the internals manually e.g.:
 *  
 *  public Person(Person source) {
 *      Name = source.name;
 *      ...
 *  }
 * 
 * The following examples show serialisation used to deep copy objects. This is not the fastest implementation,
 * for a more in depth view, see:
 * 
 * https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net-c-specifically
 * 
 */

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DesignPatterns.Patterns.Prototype {

    internal sealed class AllowAllAssemblyVersionsDeserializationBinder : SerializationBinder {

        /// <inheritdoc />
        /// <summary>
        /// Use the current assembly to create the type
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public override Type BindToType(string assemblyName, string typeName) {
            return Type.GetType($"{typeName}, {Assembly.GetExecutingAssembly().FullName}");
        }

    }

    public static class ExtensionMethods {
    
        /// <summary>
        /// If using binary serialisation on an object the class must be marked with the
        /// [Serializable] attribute, this includes nested properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T self) {

            using (var stream = new MemoryStream()) {

                var formatter = new BinaryFormatter {
                    Binder = new AllowAllAssemblyVersionsDeserializationBinder()
                };
                formatter.Serialize(stream, self);

                stream.Seek(0, SeekOrigin.Begin);
                var copy = formatter.Deserialize(stream);
                return (T) copy;

            }

        }

        /// <summary>
        ///     Slower but removes the reliance on the Serializable attributes
        ///     However, it requires default constructors on each class ...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T DeepCopyXml<T>(this T self) {
            using (var stream = new MemoryStream()) {
                var serialiser = new XmlSerializer(typeof(T));
                serialiser.Serialize(stream, self);
                stream.Position = 0;
                return (T) serialiser.Deserialize(stream);
            }
        }
    }
}