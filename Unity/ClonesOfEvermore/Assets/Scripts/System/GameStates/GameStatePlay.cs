using System;
using UnityEngine;

namespace StateMachines.Game.States
{ 

    public class GameStatePlay : GameState
    {
        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void Think()
        {
            if (GameManager.Instance.Dog.Link.isDead && GameManager.Instance.Human.Link.isDead)
                ((GameStateMachine)StateMachine).ChangeState("GameStateEnd");
        }
    }

}