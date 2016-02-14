using System;
using UnityEngine;

namespace StateMachines.Game
{

    public abstract class GameState : State
    {

    }

    /// <summary>
    /// Determines on which state the game is in
    /// </summary>
    public class GameStateMachine : StateMachine<GameState>
    {

    }

}