using System;
using UnityEngine;

using StateMachines.AI;
using StateMachines.AI.States;

public class ZombieCharacter : VisualCharacter
{
    AIStateMachine Behaviour;

    public float attackRange;
    public float chaseRange;

    AIStateZombie m_state;

    void Awake()
    {
        Behaviour = new AIStateMachine(this);
        m_state = new AIStateZombie(this);
        Behaviour.AddState(m_state);
        Behaviour.ChangeState(m_state);
    }

    protected override void Think()
    {
        Behaviour.UpdateState();

        if (m_state.attackRange != attackRange)
            m_state.attackRange = attackRange;

        if (m_state.chaseRange != chaseRange)
            m_state.chaseRange = chaseRange;
    }

    protected override void Begin()
    {
        Debug.Log("begin");

        base.Begin();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

