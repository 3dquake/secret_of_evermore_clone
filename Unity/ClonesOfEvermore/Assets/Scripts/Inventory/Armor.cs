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

    public int Defence { get; set; }
}

