﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Creates other managers, holds all static info and inventory, creates visual prefabs.
/// </summary>
[AddComponentMenu("Game Manager")]
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

    /// <summary>
    /// Access to Characters
    /// </summary>
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

    //Initialization tells how gamemanager will be initialized
    public InitLayer Initialization = InitLayer.Awake;
    public enum InitLayer
    {
        Start, Awake, Enabled
    }

    void Initialize()
    {
        m_characters = new CharacterManager();
        m_ui = new UIManager();

        Characters.FindAllCharacters();
        UI.FindAllPanels();

    }

    public void Spawn(string name)
    {

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