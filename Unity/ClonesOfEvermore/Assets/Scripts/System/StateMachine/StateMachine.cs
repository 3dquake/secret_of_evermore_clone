using System;
using System.Collections.Generic;

namespace StateMachines
{

    /// <summary>
    /// Abstract StateMachine class
    /// </summary>
    /// <typeparam name="T">Type of states this machine will handle</typeparam>
    public abstract class StateMachine<T> where T : State
    {
        public StateMachine()
        {
            m_listStates = new List<T>();
        }

        public delegate void StateMachineEvent();

        public event StateMachineEvent onChangeState;

        T m_current, m_next;

        List<T> m_listStates;

        public string Current
        {
            get
            {
                return m_current.GetType().Name;
            }
        }

        public void AddStatesRange(params T[] states)
        {
            foreach (T state in states)
            {
                AddState(state);
            }
        }

        public void AddState(T state)
        {
            if (!m_listStates.Contains(state))
            {
                if (state.Name == "" || state.Name == null)
                    state.Name = state.GetType().Name;

                state.StateMachine = this;
                m_listStates.Add(state);
            }
        }

        public void RemoveState(T state)
        {
            if (m_listStates.Contains(state))
                m_listStates.Remove(state);
        }

        /// <summary>
        /// Change to the specified state, if allowed
        /// </summary>
        /// <param name="state">State to change to</param>
        /// <returns>True if successful</returns>
        public bool ChangeState(T state)
        {
            if (!m_listStates.Contains(state))
                return false;

            if (m_current == null)
            {
                m_next = state;
                return true;
            }

            if (m_current.IsAllowed(state))
            {
                m_next = state;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes to specified state, if allowed and found
        /// </summary>
        /// <param name="name">Name of the state. Default name is the type name</param>
        /// <returns>True if changes</returns>
        public bool ChangeState(string name)
        {
            T state = m_listStates.Find(x => x.Name == name);
            if (state != null)
            {
                m_next = state;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the current state and/or sets the next state
        /// </summary>
        public virtual void UpdateState()
        {
            if (m_current != m_next)
            {
                if (onChangeState != null)
                    onChangeState();

                if (m_current != null)
                    m_current.Exit();
                m_current = m_next;
                m_current.Enter();
            }

            if (m_current != null)
                m_current.Think();

        }

    } 
}