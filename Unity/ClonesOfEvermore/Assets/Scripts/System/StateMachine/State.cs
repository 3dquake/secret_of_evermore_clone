using System;
using System.Collections.Generic;

namespace StateMachines
{
    /// <summary>
    /// Abstract state class for FSMs
    /// </summary>
    public abstract class State
    {
        public State(params State[] states)
        {
            if (states != null)
            {
                m_states = new List<State>(states.Length);
                m_states.AddRange(states);
            }
            else
            {
                m_states = new List<State>(2);
            }
        }

        /// <summary>
        /// Name of the state
        /// </summary>
        public string Name { get; set; }
        //List of allowed states
        List<State> m_states;

        public object StateMachine { get; set; }

        /// <summary>
        /// Is the specified state allowed?
        /// </summary>
        /// <param name="state"></param>
        /// <returns>True if allowed</returns>
        public bool IsAllowed(State state)
        {
            return m_states.Contains(state);
        }

        /// <summary>
        /// Add a state
        /// </summary>
        /// <param name="state"></param>
        public void AddState(State state)
        {
            if (!m_states.Contains(state))
                m_states.Add(state);
        }

        /// <summary>
        /// Remove a state
        /// </summary>
        /// <param name="state"></param>
        public void RemoveState(State state)
        {
            if (m_states.Contains(state))
                m_states.Remove(state);
        }

        /// <summary>
        /// Clear states
        /// </summary>
        public void Clear()
        {
            m_states.Clear();
        }

        public abstract void Enter();
        public abstract void Think();
        public abstract void Exit();

    } 
}