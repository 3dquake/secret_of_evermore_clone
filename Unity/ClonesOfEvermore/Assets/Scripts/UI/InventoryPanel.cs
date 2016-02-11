using System;
using UnityEngine;

public class InventoryPanel : EvermorePanel
{
    public override void Refresh()
    {
        
    }

    public override void Initialize()
    {
        Debug.Log("Initialized panel '"+name+"'",this);
    }
}

