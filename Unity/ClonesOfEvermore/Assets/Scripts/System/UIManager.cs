using UnityEngine;
using System.Collections.Generic;

public class UIManager {
    
    List<EvermorePanel> m_panels;

    public void Refresh()
    {
        foreach (EvermorePanel panel in m_panels)
        {
            //panel.Refresh();
        }
    }

}
