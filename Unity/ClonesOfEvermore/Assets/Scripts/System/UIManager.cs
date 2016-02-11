using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager {
    
    List<EvermorePanel> m_panels;
    Canvas m_root;

    /// <summary>
    /// Default constructor.
    /// UIManager will initialize root and lists, and finds all panels in root.
    /// </summary>
    public UIManager()
    {
        m_root = GameObject.FindObjectOfType<Canvas>();
        EvermorePanel[] panels = m_root.GetComponentsInChildren<EvermorePanel>();

        m_panels = new List<EvermorePanel>(panels.Length);
        m_panels.AddRange(panels);
        m_panels.ForEach(InitializePanels);

    }

    void SetActive(EvermorePanel panel, bool active)
    {
        panel.gameObject.SetActive(active);
    }

    void InitializePanels(EvermorePanel panel)
    {
        panel.Initialize();
        if (panel.hideOnStart)
            panel.Hide();
    }


    public void ShowPanel(string name)
    {
        SetActive(m_panels.Find(x => x.name == name), true);
    }

    public void HidePanel(string name)
    {
        SetActive(m_panels.Find(x => x.name == name), false);
    }

    public void HideAll()
    {
        foreach (EvermorePanel panel in m_panels)
        {
            panel.Hide();
        }
    }

    public void TogglePanel(string name)
    {
        EvermorePanel panel = m_panels.Find(x => x.gameObject.name == name);
        if (panel == null)
        {
            Debug.LogWarning("Warning: Panel '"+name+"' does not exist");
            return;
        }
        SetActive(panel, !panel.gameObject.activeSelf);
    }
    
    public void RefreshPanels()
    {
        if (m_panels == null)
            return;

        foreach (EvermorePanel panel in m_panels)
        {
            //if (panel.dirty)
            panel.Refresh();
        }
    }

    public EvermorePanel GetPanel(string name)
    {
        return m_panels.Find(x => x.name == name);
    }

    public /*EvermorePanel[]*/void RefreshLists()
    {
        if (!m_root)
            return;

        EvermorePanel[] panels = m_root.GetComponentsInChildren<EvermorePanel>();

        m_panels.Clear();
        m_panels.Capacity = panels.Length;

        m_panels.AddRange(panels);
        m_panels.ForEach(InitializePanels);

        m_panels.ForEach(PrintPanels);
        //return panels;
    }

    void PrintPanels(EvermorePanel panel)
    {
        Debug.Log(panel.name);
    }

}
