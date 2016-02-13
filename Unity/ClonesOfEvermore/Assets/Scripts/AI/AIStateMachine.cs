using UnityEngine;

using System;
using System.Collections.Generic;

using StateMachines;

namespace StateMachines.AI
{
    public abstract class AIState : State
    {
        public AIState(VisualCharacter character)
        {
            Character = character;
        }
        public VisualCharacter Character { get; set; }
    }

    public class AIStateMachine : StateMachine<AIState>
    {
        public AIStateMachine(VisualCharacter character)
        {
            Character = character;
        }

        public VisualCharacter Character { get; private set; }
    } 
}