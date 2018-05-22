/*
 * A pattern in which the object's behaviour is determined by its state. An object
 * transitions from one state to another (something needs to trigger a transition).
 *
 * A formalized construct which manages state and transitions is called a state machine.
 * State machines are normally implemented with specific state machine libraries e.g: Stateless (Autofac author),
 * Microsoft Workflow Foundation.
 *
 * Changes in state can be explicit or in response to an event (Observer pattern)
 *
 * Can define:
 *
 *  State entry/exit behaviours
 *  Action when a particular event causes a transition
 *  Guard conditions enabling/disabling a transition
 *  Default action when no transitions are found for an event
 *
 */

using System.Collections.Generic;

namespace DesignPatterns.Patterns.State {

    internal class BehaviouralState {

        public enum State {
            OffHook,
            Connecting,
            Connected,
            OnHold
        }

        public enum Trigger {
            CallDialed,
            HungUp,
            CallConnected,
            PlacedOnHold,
            TakenOffHold,
            LeftMessage
        }

        private static Dictionary<State, List<(Trigger, State)>> _rules
            = new Dictionary<State, List<(Trigger, State)>> {
                [State.OffHook] = new List<(Trigger, State)> {
                    (Trigger.CallDialed, State.Connecting)
                },
                [State.Connecting] = new List<(Trigger, State)> {
                    (Trigger.HungUp, State.OffHook),
                    (Trigger.CallConnected, State.Connected)
                },
                [State.Connected] = new List<(Trigger, State)> {
                    (Trigger.LeftMessage, State.OffHook),
                    (Trigger.HungUp, State.OffHook),
                    (Trigger.PlacedOnHold, State.OnHold)
                },
                [State.OnHold] = new List<(Trigger, State)> {
                    (Trigger.TakenOffHold, State.Connected),
                    (Trigger.HungUp, State.OffHook)
                }
            };

    }

}