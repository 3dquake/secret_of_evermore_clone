using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Abstract StateMachine class
/// </summary>
/// <typeparam name="T">Type of states this machine will handle</typeparam>
public abstract class StateMachine<T> where T : State
{
    public StateMachine() { }

    public delegate void StateMachineEvent();

    public event StateMachineEvent onChangeState;

    T m_current, m_next;

    /// <summary>
    /// Change to the specified state, if allowed
    /// </summary>
    /// <param name="state">State to change to</param>
    /// <returns>True if successful</returns>
    public bool ChangeState(T state)
    {
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