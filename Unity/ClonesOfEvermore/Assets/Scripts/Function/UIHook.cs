using UnityEngine;
using System.Collections;

/// <summary>
/// UIHook class. Gives UI elements access to UI functionalities
/// </summary>
[AddComponentMenu("Special/UI Hook")]
public class UIHook : MonoBehaviour {

    GameManager m_game;

    void Awake()
    {
        if (!m_game)
            m_game = FindObjectOfType<GameManager>();
        if (!m_game)
            Debug.LogError("Error: No GameManager found!");
    }

    public void TogglePanel(string name)
    {
        if (m_game)
            m_game.UI.TogglePanel(name);
    }

    public void HidePanel(string name)
    {
        if (m_game)
            m_game.UI.HidePanel(name);
    }

    public void ShowPanel(string name)
    {
        if (m_game)
            m_game.UI.ShowPanel(name);
    }

    public void HideAll()
    {
        if (m_game)
            m_game.UI.HideAll();
    }



}
