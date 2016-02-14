using System;
using UnityEngine;

/// <summary>
/// Base class for Armor items
/// </summary>
public abstract class Armor : Item
{
    public Armor()
    {
        Type = ItemType.Armor;
    }

    public Armor(string name, string desc, int worth) 
        : base(name, desc, worth, 1)
    {
        Type = ItemType.Armor;
    }

    public int Defence { get; protected set; }
    public Character Owner { get; set; }

    public virtual void OnEquip()
    {
        Debug.LogFormat("'{0}' equipped '{1}'", Owner.Name, Name);
    }
    public virtual void OnDequip()
    {
        Debug.LogFormat("'{0}' dequipped '{1}'", Owner.Name, Name);
        Owner = null;
    }

}

