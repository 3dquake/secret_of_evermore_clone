using System;
using UnityEngine;

namespace StateMachines.Game.States
{

    public class GameStateInit : GameState
    {
        public override void Enter()
        {
            GameManager.Instance.Characters.FindAllCharacters();

            GameManager.Instance.Dog = GameManager.Instance.Characters.FindCharacter(x => x.Link.name == "Character.Dog");
            GameManager.Instance.Human = GameManager.Instance.Characters.FindCharacter(x => x.Link.name == "Character.Human");

            GameManager.Instance.UI.FindAllPanels();

            GameManager.Instance.cameraTargets = new GameObject[] { GameManager.Instance.Human.Link.gameObject, GameManager.Instance.Dog.Link.gameObject };
            GameManager.Instance.Camera.FindCameras(GameManager.Instance.cameraTargets);
            GameManager.Instance.Camera.ChangeTarget("Character.Human");

            GameManager.Instance.Camera.AddState(new CameraStateFollow(GameManager.Instance.movementSmooth, GameManager.Instance.rotationSmooth, GameManager.Instance.minY, GameManager.Instance.maxY));
            GameManager.Instance.Camera.ChangeState("CameraStateFollow");

            GameManager.Instance.UI.RefreshAllPanels();

            // Create new inventory
            //if (GameManager.Instance.Inventory == null)
            //    GameManager.Instance.Inventory = new Inventory(GameManager.Instance.slots);

            //// Create new CharacterManager
            //if (GameManager.Instance.Characters == null)
            //{
            //    GameManager.Instance.Characters = new CharacterManager();

            //    // Find characters
            //    GameManager.Instance.Dog = GameManager.Instance.Characters.FindCharacter(x => x.Link.name == "Character.Dog");
            //    GameManager.Instance.Human = GameManager.Instance.Characters.FindCharacter(x => x.Link.name == "Character.Human");

            //}

            //// Create new CameraController
            //if (GameManager.Instance.Camera == null)
            //{
            //    GameManager.Instance.Camera = new CameraController(/*GameManager.Instance.Human.Link.gameObject, GameManager.Instance.Dog.Link.gameObject*/);
            //    // Add states and change
            //    GameManager.Instance.Camera.AddState(new CameraStateFollow(GameManager.Instance.movementSmooth, GameManager.Instance.rotationSmooth, GameManager.Instance.minY, GameManager.Instance.maxY));
            //    GameManager.Instance.Camera.ChangeState("CameraStateFollow");
            //}

            //// Create a new UI Manager
            //if (GameManager.Instance.UI == null)
            //    GameManager.Instance.UI = new UIManager();
        }

        public override void Exit()
        {

        }

        public override void Think()
        {
            if (GameManager.Instance.UI.AllowMultiple != GameManager.Instance.allowMultiplePanels)
                GameManager.Instance.UI.AllowMultiple = GameManager.Instance.allowMultiplePanels;

            // Refresh opened panels
            GameManager.Instance.UI.RefreshPanels();
            GameManager.Instance.Camera.Update();
        }
    }

}