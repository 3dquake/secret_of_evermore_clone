using UnityEngine;

using System;
using System.Collections.Generic;

public abstract class AIState
{
    public AIState(params AIState[] states)
    {
        if (states != null)
        {
            m_states = new List<AIState>(states.Length);
            m_states.AddRange(states);
        }
        else
        {
            m_states = new List<AIState>(2);
        }
    }

    /// <summary>
    /// Name of the state
    /// </summary>
    public string Name { get; set; }
    //List of allowed states
    List<AIState> m_states;

    public bool IsAllowed(AIState state)
    {
        return m_states.Contains(state);
    }

    public void AddState(AIState state)
    {
        if (!m_states.Contains(state))
            m_states.Add(state);
    }

    public void RemoveState(AIState state)
    {
        if (m_states.Contains(state))
            m_states.Remove(state);
    }

    public void Clear()
    {
        m_states.Clear();
    }

    public abstract void Enter();
    public abstract void Think();
    public abstract void Exit();

}

public class AIBehaviour
{
    public AIBehaviour() { }

    public delegate void AIEvent();
    
    public AIEvent onChangeState { get; set; }

    AIState m_current, m_next;

    /// <summary>
    /// Change the AI's state
    /// </summary>
    /// <param name="state">State to change to</param>
    /// <returns>True if successful</returns>
    public bool ChangeState(AIState state)
    {
        if (state.IsAllowed(state))
        {
            m_next = state;
            return true;
        }
        return false;
    }

    public void Refresh()
    {
        //Don't do anything if we don't have a current state to work on
        if (m_current == null)
            return;

        if (m_current != m_next)
        {
            if (onChangeState != null)
                onChangeState();

            m_current.Exit();
            m_current = m_next;
            m_current.Enter();
        }

        m_current.Think();

    }

}