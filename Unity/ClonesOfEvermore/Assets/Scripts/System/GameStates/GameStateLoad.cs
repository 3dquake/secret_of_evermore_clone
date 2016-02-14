using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachines.Game.States
{
    public class GameStateLoad : GameState
    {
        Scene scene;

        public override void Enter()
        {
            SceneManager.LoadScene("level", LoadSceneMode.Single);
            scene = SceneManager.GetSceneByName("level");
        }

        public override void Exit()
        {

        }

        public override void Think()
        {
            if (scene.isLoaded)
            {
                GameManager.Instance.ChangeState("GameStatePlay");

                GameManager.Instance.Characters.FindAllCharacters();

                GameManager.Instance.Dog = GameManager.Instance.Characters.FindCharacter(x => x.Link.name == "Character.Dog");
                GameManager.Instance.Human = GameManager.Instance.Characters.FindCharacter(x => x.Link.name == "Character.Human");

                GameManager.Instance.UI.FindAllPanels();

                //// Create new inventory
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
                //    GameManager.Instance.Camera = new CameraController(GameManager.Instance.Human.Link.gameObject, GameManager.Instance.Dog.Link.gameObject);
                //    // Add states and change
                //    GameManager.Instance.Camera.AddState(new CameraStateFollow(GameManager.Instance.movementSmooth, GameManager.Instance.rotationSmooth, GameManager.Instance.minY, GameManager.Instance.maxY));
                //    GameManager.Instance.Camera.ChangeState("CameraStateFollow");
                //}



            }
        }
    }
}
