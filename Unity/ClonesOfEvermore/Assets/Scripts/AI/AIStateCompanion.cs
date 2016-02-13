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

        public GameObject target { get; set; }

        public float maxRange { get; set; }
        public float minRange { get; set; }


        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void Think()
        {
            //Debug.Log(Character.name + " " + target.name);


            if (target != null)
            {
                float distance = Vector3.Distance(Character.transform.position, target.transform.position);
                if (distance > maxRange)
                    Character.Follow(target.transform.position, 0.5f);

            }
        }
    }


}