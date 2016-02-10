using UnityEngine;
using System.Collections.Generic;

public class UIManager {
    
    List<EvermorePanel> m_panels;

    void SetActive(EvermorePanel panel, bool active)
    {
        panel.gameObject.SetActive(active);
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
        EvermorePanel panel = m_panels.Find(x => x.name == name);
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

    public EvermorePanel[] FindAllPanels()
    {
        EvermorePanel[] panels = Object.FindObjectsOfType<EvermorePanel>();
        m_panels = new List<EvermorePanel>(panels.Length);
        Debug.Log(panels.Length);
        m_panels.AddRange(panels);
        return panels;
    }

}
