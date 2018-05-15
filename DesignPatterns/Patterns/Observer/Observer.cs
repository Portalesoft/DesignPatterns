/*
 * An observer is an object that wished to be informed about events happening in the
 * system. The entity generating the events is an observable.
 *
 * We need to be informed when certain things happen
 *
 *   Object's property changes
 *   Object does soemthing
 *   Some external event occurs
 *
 * We want to listen to events and be notified when they occur
 * Built into C# with the event keyword
 *
 * Reactive Extensions provide IObservable<T>/IObserver<T> in stream processing
 *
 * Also, INotifyPropertyChanging/Changed and BindingList<T>/ObservableCollection<T>
 *
 */

using System;

namespace DesignPatterns.Patterns.Observer {

    public class FallsIllEventArgs {
        public string Address;
    }

    public class Person {

        public event EventHandler<FallsIllEventArgs> FallsIll;
        public void CatchACold() {
            FallsIll?.Invoke(this,
                new FallsIllEventArgs { Address = "123 London Road" });
        }

    }

}