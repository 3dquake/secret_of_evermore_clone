using UnityEngine;
using System.Collections;

//Creates other managers, holds all static info, holds inventory, creates visual prefabs based on the info from other managers
public class GameManager : MonoBehaviour {
    
    /// <summary>
    /// Static access
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (!m_gm)
                m_gm = FindObjectOfType<GameManager>();

            return m_gm;

        }
    }
    static GameManager m_gm;

    public CharacterManager Characters
    {
        get
        {
            //if (m_ui == null)
            //    m_ui = new UIManager();

            return m_characters;
        }
    }
    CharacterManager m_characters;

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

    public Inventory Inventory
    {
        get
        {
            return m_inventory;
        }
    }
    Inventory m_inventory;

    public InitLayer Initialization = InitLayer.Awake;
    public enum InitLayer
    {
        Start, Awake, Enabled
    }

    void Initialize()
    {
        m_characters = new CharacterManager();
        m_ui = new UIManager();
    }

    #region MonoBehaviour

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
