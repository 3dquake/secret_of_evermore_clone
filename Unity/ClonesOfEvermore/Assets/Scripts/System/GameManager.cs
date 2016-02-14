using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

using StateMachines.Game;
using StateMachines.Game.States;

/// <summary>
/// Creates other managers, holds all static info and inventory, creates visual prefabs.
/// </summary>
[AddComponentMenu("Game Manager")]
public class GameManager : MonoBehaviour
{

    #region Systems

    /// <summary>
    /// Global access
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (!m_gm)
                m_gm = FindObjectOfType<GameManager>();

            if (!m_gm)
                Debug.LogError("Error! No GameManager found!");

            return m_gm;

        }
    }
    static GameManager m_gm;

    /// <summary>
    /// Describes the game's current state
    /// </summary>
    public GameStateMachine GameState
    {
        get
        {
            return m_gameState;
        }
        private set
        {
            m_gameState = value;
        }
    }
    GameStateMachine m_gameState;

    /// <summary>
    /// Access to Characters
    /// </summary>
    public CharacterManager Characters
    {
        get
        {
            return m_characters;
        }
    }
    CharacterManager m_characters;

    /// <summary>
    /// Access to UI
    /// </summary>
    public UIManager UI
    {
        get
        {
            return m_ui;
        }
    }
    UIManager m_ui;

    /// <summary>
    /// Access to player inventory
    /// </summary>
    public Inventory Inventory
    {
        get
        {
            return m_inventory;
        }
    }
    Inventory m_inventory;

    /// <summary>
    /// Access to camera controls
    /// </summary>
    public CameraController Camera
    {
        get
        {
            return m_camera;
        }
    }
    CameraController m_camera;

    #endregion

    //

    #region Settings

    [Header("Game manager settings")]
    public string startState; // GameStateMachine starting state
    public float KillHeight; // KillHeight kills characters falling below this Y value
    public InitLayer Initialization = InitLayer.Awake;
    public UpdateLayer Updating = UpdateLayer.Update;

    [Header("UI Manager settings")]
    public bool allowMultiplePanels; // Multiple panels showing?
    public Sprite emptyIcon;

    [Header("Inventory settings")]
    public int slots = 12; // Maximum slots in inventory
    [ColorUsage(true)]
    public Color normalColor, equippedColor; // Colors

    [Header("Camera settings")]
    public GameObject[] cameraTargets; // Targets for camera to follow
    public float movementSmooth, rotationSmooth, minY, maxY; // Camera movement settings

    #endregion

    //

    #region Static fields

    /// <summary>
    /// Quick global access to dog character
    /// </summary>
    public Character Dog
    {
        get { return m_dog; }
        set { if (m_dog == null) m_dog = value; }
    }
    Character m_dog;

    /// <summary>
    /// Quick global access to human character
    /// </summary>
    public Character Human
    {
        get { return m_human; }
        set { if (m_human == null) m_human = value; }
    }
    Character m_human;

    #endregion

    //
    //
    //

    #region GameManager

    //Initialization tells how gamemanager will be initialized
    public enum InitLayer
    {
        Start, Awake, Enabled
    }

    // Updatelayers; When do we update?
    public enum UpdateLayer
    {
        Update, FixedUpdate, LateUpdate
    }

    void Initialize()
    {
        // Prevents duplicate GameManagers, but allows for easier testing
        if (Instance != FindObjectOfType<GameManager>())
        {
            gameObject.SetActive(false);
            return;
        }

        // Don't destroy this; other GameManagers in other scenes should be disabled before build and for testing should use GameStateInit
        DontDestroyOnLoad(this);

        // Create new inventory
        if (m_inventory == null)
        {
            m_inventory = new Inventory(slots);
        }

        ///////////////////////

        // Create new CharacterManager
        if (m_characters == null)
        {
            // No auto; let game states search by their own
            m_characters = new CharacterManager(false);
        }

        ///////////////////////

        // Create new CameraController
        if (m_camera == null)
        {
            // Add targets after scene load
            m_camera = new CameraController(/*Human.Link.gameObject, Dog.Link.gameObject*/);

            // Add states, but change on GameStatePlay
            m_camera.AddState(new CameraStateFollow(movementSmooth, rotationSmooth, minY, maxY));
            //Camera.ChangeState("CameraStateFollow");
        }

        ///////////////////////

        // Create a new UI Manager
        if (m_ui == null)
        {
            //BUG: this seems to be null'd after first scene load?
            m_ui = new UIManager(false);
        }

        ///////////////////////

        // Create new GameStateMachine
        if (GameState == null)
        {
            GameState = new GameStateMachine();

            // Add some states
            GameState.AddStatesRange(new GameStatePlay(), new GameStateStart(), new GameStateEnd(), new GameStateLoad(), new GameStateInit());
            GameState.ChangeState(startState); // Set starting statea
            GameState.UpdateState(); // Update to that state immidiately

        }

        ///////////////////////

        // Refresh GameManager once after all systems are initialized
        Refresh();

    }

    // Updates this GameManager
    void Refresh()
    {
        GameState.UpdateState();
    }

    #endregion GameManager

    //
    //
    //

    #region Hooks

    /// <summary>
    /// Changes GameState to specified state, if available
    /// </summary>
    /// <param name="name">Name of the state</param>
    public void ChangeState(string name)
    {
        GameState.ChangeState(name);
    }

    /// <summary>
    /// Load a scene, if available
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    /// <summary>
    /// Quit the application
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    //public void UITogglePanel(string name)
    //{
    //    UI.TogglePanel(name);
    //}

    //public void UIShowPanel(string name)
    //{
    //    UI.ShowPanel(name);
    //}

    //public void UIHidePanel(string name)
    //{
    //    UI.HidePanel(name);
    //}

    #endregion

    //
    //
    //

    #region MonoBehaviour

    void Update()
    {
        if (Updating == UpdateLayer.Update)
            Refresh();
    }

    void FixedUpdate()
    {
        if (Updating == UpdateLayer.FixedUpdate)
            Refresh();
    }

    void LateUpdate()
    {
        if (Updating == UpdateLayer.LateUpdate)
            Refresh();
    }

    void Start()
    {
        if (Initialization == InitLayer.Start)
            Initialize();
    }

    void Awake()
    {
        if (Initialization == InitLayer.Awake)
            Initialize();
    }

    void OnEnabled()
    {
        if (Initialization == InitLayer.Enabled)
            Initialize();
    }

    #endregion MonoBehaviour

}
