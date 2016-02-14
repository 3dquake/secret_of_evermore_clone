using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Manager class for UI elements
/// </summary>
public class UIManager {
    
    List<EvermorePanel> m_panels;
    Canvas m_root;

    EvermorePanel m_temp = null;

    /// <summary>
    /// Tells the manager to allow to show multiple UI panels;
    /// </summary>
    public bool AllowMultiple { get; set; }

    /// <summary>
    /// Default constructor.
    /// UIManager will initialize root and lists, and finds all panels in root.
    /// </summary>
    public UIManager(bool auto = true)
    {
        if (auto)
        {
            FindAllPanels();

            // Panel handling, initialize them, and refresh them once
            InitializeAllPanels();
            RefreshAllPanels();
        }
    }

    public void FindAllPanels()
    {
        // Find all panels from the root canvas
        if (!m_root)
            m_root = GameObject.FindObjectOfType<Canvas>();

        // Double check
        if (!m_root)
        {
            Debug.LogWarning("Warning: No suitable canvas root found!");
            return;
        }

        EvermorePanel[] panels = m_root.GetComponentsInChildren<EvermorePanel>();

        // Initialize lists, and add the panels
        m_panels = new List<EvermorePanel>(panels.Length);
        m_panels.AddRange(panels);

        InitializeAllPanels();
        RefreshAllPanels();
    }

    /// <summary>
    /// Shorthand for disabling gameObjects
    /// </summary>
    /// <param name="panel">Panel to set</param>
    /// <param name="active">State</param>
    void SetActive(EvermorePanel panel, bool active)
    {
        panel.SetActive(active);
    }

    /// <summary>
    /// Initializes loaded panels, regardless are they active or not
    /// </summary>
    /// <param name="panel"></param>
    void InitializeAllPanels()
    {
        foreach (EvermorePanel panel in m_panels)
        {
            panel.Initialize();
            if (panel.hideOnStart)
                panel.Hide();
        }
    }
    
    /// <summary>
    /// Display the specified panel
    /// </summary>
    /// <param name="name">Panel to display</param>
    public void ShowPanel(string name)
    {
        // Checks if there is already a panel open
        if (m_temp && !AllowMultiple)
            m_temp.Hide(); // Hides it if multiples are prohibited

        m_temp = m_panels.Find(x => x.name == name);

        m_temp.Show();
        m_temp.Refresh();
    }

    /// <summary>
    /// Hide the specified panel
    /// </summary>
    /// <param name="name">The panel to hide</param>
    public void HidePanel(string name)
    {
        m_panels.Find(x => x.name == name).Hide();
    }

    /// <summary>
    /// Hide all panels
    /// </summary>
    public void HideAll()
    {
        foreach (EvermorePanel panel in m_panels)
        {
            panel.Hide();
        }
    }

    /// <summary>
    /// Toggle a panel's state
    /// </summary>
    /// <param name="name">Panel name</param>
    public void TogglePanel(string name)
    {
        EvermorePanel temp = m_panels.Find(x => x.gameObject.name == name);

        if (m_temp && !AllowMultiple && temp != m_temp)
            m_temp.Hide();

        m_temp = temp;
        if (m_temp == null)
        {
            Debug.LogWarningFormat("Warning: Panel '{0}' does not exist!", name);
            return;
        }
        m_temp.Toggle();
    }
    
    /// <summary>
    /// Refreshes all panels, regardless are they active or not
    /// </summary>
    public void RefreshAllPanels()
    {
        if (m_panels == null || m_panels.Count == 0)
            return;

        foreach (EvermorePanel panel in m_panels)
        {
            panel.Refresh();
        }
    }

    /// <summary>
    /// Refreshes active panels
    /// </summary>
    public void RefreshPanels()
    {
        // Lets not refresh anything if we don't have anything to refresh
        if (m_panels == null || m_panels.Count == 0)
            return;

        foreach (EvermorePanel panel in m_panels)
        {
            if (panel.Active)
                panel.Refresh();
        }
    }

    /// <summary>
    /// Finds a panel and returns it
    /// </summary>
    /// <param name="name">Panel name</param>
    /// <returns>The panel</returns>
    public EvermorePanel GetPanel(string name)
    {
        return m_panels.Find(x => x.name == name);
    }

    /// <summary>
    /// Refreshes internal lists and reinitializes them
    /// </summary>
    public /*EvermorePanel[]*/void RefreshLists()
    {
        if (!m_root)
            return;

        EvermorePanel[] panels = m_root.GetComponentsInChildren<EvermorePanel>();

        m_panels.Clear();
        m_panels.Capacity = panels.Length;

        m_panels.AddRange(panels);
        InitializeAllPanels();

        //m_panels.ForEach(PrintPanels);
        //return panels;
    }

    //void PrintPanels(EvermorePanel panel)
    //{
    //    Debug.Log(panel.name);
    //}

}
