using System;
using System.Collections.Generic;

using UnityEngine;

namespace StateMachines.AI.States
{
    /// <summary>
    /// Zombie AI. Stays dormant and slowly walks to targets
    /// </summary>
    public class AIStateZombie : AIState
    {
        public AIStateZombie(VisualCharacter character) : base(character)
        {
        }

        public float attackRange { get; set; }
        public float chaseRange { get; set; }

        float distance;
        VisualCharacter m_target = null;
        //Vector3 m_direction;

        //void Search(Vector3 position, float radius, string tag)
        //{
        //    foreach (Collider target in Physics.OverlapSphere(Character.transform.position, chaseRange))
        //    {
        //        if (target.tag == tag)
        //        {
        //            m_target = target.GetComponent<VisualCharacter>();
        //            break;
        //        }
        //    }

        //}

        void Chase()
        {
            Character.Follow(m_target.transform.position, 0f);

            //float distance = Vector3.Distance(Character.transform.position, m_target.transform.position);

            //m_direction = m_target.transform.position - Character.transform.position;
            //m_direction.y = 0;
            //Character.Move(m_direction.normalized);

            if (distance > chaseRange)
                m_target = null;
            else if (distance <= attackRange && !m_target.isDead)
            {
                m_target.Hurt(Character.Link.Attack);
            }
        }

        public override void Enter()
        {

        }
        public override void Exit()
        {

        }
        public override void Think()
        {
            // Search for a target 'til seen
            if (m_target == null)
                m_target = Character.Search(Character.transform.position, chaseRange, "Player");

            // Chase the target!
            if (m_target != null)
            {
                distance = Vector3.Distance(Character.transform.position, m_target.transform.position);
                Chase();
            }

        }
    }


}