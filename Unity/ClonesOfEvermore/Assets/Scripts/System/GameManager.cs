using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System;

using StateMachines.Game;
using StateMachines.Game.States;

/// <summary>
/// Creates other managers, holds all static info and inventory, creates visual prefabs.
/// </summary>
[AddComponentMenu("Game Manager")]
public class GameManager : MonoBehaviour
{

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
    /// Access to Characters
    /// </summary>
    public CharacterManager Characters
    {
        get
        {
            //if (m_characters == null)
            //    m_characters = new CharacterManager();

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
            //if (m_ui == null)
            //    m_ui = new UIManager();

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

    public CameraController Camera
    {
        get
        {
            return m_camera;
        }
    }
    CameraController m_camera;

    [Header("UI Manager settings")]
    public bool allowMultiplePanels;
    public Sprite emptyIcon;

    [Header("Inventory settings")]
    public int slots = 12;
    [ColorUsage(true)]
    public Color normalColor, equippedColor;

    [Header("Camera settings")]
    public GameObject[] cameraTargets;
    public float movementSmooth, rotationSmooth, minY, maxY;

    /// <summary>
    /// Quick global access to dog character
    /// </summary>
    public Character Dog { get; private set; }
    /// <summary>
    /// Quick global access to human character
    /// </summary>
    public Character Human { get; private set; }

    #region GameManager

    [Header("Game manager settings")]
    public float KillHeight;
    //Initialization tells how gamemanager will be initialized
    public InitLayer Initialization = InitLayer.Awake;
    public enum InitLayer
    {
        Start, Awake, Enabled
    }

    public UpdateLayer Updating = UpdateLayer.Update;
    public enum UpdateLayer
    {
        Update, FixedUpdate, LateUpdate
    }

    void Initialize()
    {
        //if (FindObjectOfType<GameManager>())
        //{
        //    Destroy(this);
        //}

        DontDestroyOnLoad(this);

        // Create new GameStateMachine
        if (GameState == null)
            GameState = new GameStateMachine();

        // Add some states
        GameState.AddStatesRange(new GameStatePlay(), new GameStateStart(), new GameStateEnd());

        // Create new inventory
        if (Inventory == null)
            m_inventory = new Inventory(slots);

        // Create new CharacterManager
        if (Characters == null)
            m_characters = new CharacterManager();

        // Find characters
        Dog = Characters.FindCharacter(x => x.Link.name == "Character.Dog");
        Human = Characters.FindCharacter(x => x.Link.name == "Character.Human");

        // Create new CameraController
        if (Camera == null)
            m_camera = new CameraController(cameraTargets);

        // Create new States and request state change
        CameraStateFollow cameraStateFollow = new CameraStateFollow(movementSmooth, rotationSmooth, minY, maxY);
        Camera.AddState(cameraStateFollow);
        Camera.ChangeState(cameraStateFollow);

        // Create a new UI Manager
        if (UI == null)
            m_ui = new UIManager();

        // Refresh GameManager once
        Refresh();

    }

    //public void OnLevelWasLoaded(int level)
    //{
    //    Initialize();
    //    Camera.Initialize();
    //}

    void Refresh()
    {
        if (UI.AllowMultiple != allowMultiplePanels)
            UI.AllowMultiple = allowMultiplePanels;

        // Refresh opened panels
        UI.RefreshPanels();

        // Update StateMachines
        Camera.Update();
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

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

    #endregion GameManager

    #region UI

    // Hook for Unity in-editor UI buttons

    public void UI_TogglePanel(string name)
    {
        UI.TogglePanel(name);
    }

    #endregion UI

}
