using System;
using UnityEngine;

namespace StateMachines.Game.States
{

    public class GameStateEnd : GameState
    {
        public override void Enter()
        {
            Debug.Log("Enter");
            if (Application.isEditor)
                UnityEditor.EditorApplication.isPlaying = false;
        }

        public override void Exit()
        {

        }

        public override void Think()
        {

        }
    }

}