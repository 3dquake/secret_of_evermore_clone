using System;
using UnityEngine;

namespace StateMachines.Game.States
{ 

    public class GameStatePlay : GameState
    {
        public override void Enter()
        {
            ////UnityEngine.SceneManagement.SceneManager.LoadScene("level", UnityEngine.SceneManagement.LoadSceneMode.Single);

            GameManager.Instance.cameraTargets = new GameObject[] { GameManager.Instance.Human.Link.gameObject, GameManager.Instance.Dog.Link.gameObject };
            GameManager.Instance.Camera.FindCameras(GameManager.Instance.cameraTargets);
            GameManager.Instance.Camera.ChangeTarget("Character.Human");

            GameManager.Instance.Camera.AddState(new CameraStateFollow(GameManager.Instance.movementSmooth, GameManager.Instance.rotationSmooth, GameManager.Instance.minY, GameManager.Instance.maxY));
            GameManager.Instance.Camera.ChangeState("CameraStateFollow");

            GameManager.Instance.UI.RefreshAllPanels();

            //GameManager.Instance.Initialize();

        }

        public override void Exit()
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        public override void Think()
        {

            if (GameManager.Instance.UI.AllowMultiple != GameManager.Instance.allowMultiplePanels)
                GameManager.Instance.UI.AllowMultiple = GameManager.Instance.allowMultiplePanels;

            // Refresh opened panels
            GameManager.Instance.UI.RefreshPanels();
            // Update camera
            GameManager.Instance.Camera.Update();


            if (GameManager.Instance.Dog.Link.isDead && GameManager.Instance.Human.Link.isDead)
            {
                GameManager.Instance.UI.ShowPanel("Panel.Gameover");
                //if (Input.GetKeyDown(KeyCode.Escape))
                //    ((GameStateMachine)StateMachine).ChangeState("GameStatePlay");
            }

        }
        
    }

}