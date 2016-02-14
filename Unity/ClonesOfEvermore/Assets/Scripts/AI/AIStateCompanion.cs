using System;
using UnityEngine;

namespace StateMachines.AI.States
{
    /// <summary>
    /// Companion AI. Follows around, attacks things
    /// </summary>
    public class AIStateCompanion : AIState
    {
        public AIStateCompanion(VisualCharacter character) : base(character)
        {

        }

        float allyDistance;
        float enemyDistance;
        
        VisualCharacter attackTarget;
        public GameObject target { get; set; }

        public float chaseRange { get; set; }
        public float attackRange { get; set; }

        public float maxRange { get; set; }
        public float minRange { get; set; }

        void Chase()
        {
            Character.Follow(attackTarget.transform.position, 0f);

            if (enemyDistance > chaseRange || attackTarget.isDead)
                attackTarget = null;
            else if (enemyDistance <= attackRange && !attackTarget.isDead)
            {
                Character.Attack();
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
            allyDistance = Vector3.Distance(target.transform.position, Character.transform.position);

            if (attackTarget != null && attackTarget.isDead)
                attackTarget = null;

            //Debug.Log(Character.name + " " + target.name);
            if (attackTarget == null)
            {
                attackTarget = Character.Search(Character.transform.position, 7f, "Enemy");
            }

            if (target != null && attackTarget == null)
            {
                enemyDistance = Vector3.Distance(Character.transform.position, target.transform.position);
                if (enemyDistance > maxRange)
                    Character.Follow(target.transform.position, 0.5f);
            }
            // Close combat
            else if (attackTarget != null && Character.Link.Weapon.GetType() != typeof(Weapon_Bow))
            {
                Chase();
            }
            //Attack with a bow
            else if (attackTarget != null && Character.Link.Weapon.GetType() == typeof(Weapon_Bow) )
            {
                Character.Look((attackTarget.transform.position - Character.transform.position).normalized);   
                Character.Attack();
            }
        }
    }


}