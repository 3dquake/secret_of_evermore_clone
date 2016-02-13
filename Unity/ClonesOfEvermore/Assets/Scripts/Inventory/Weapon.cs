using System;
using UnityEngine;

/// <summary>
/// Base class for weapon items
/// </summary>
public abstract class Weapon : Item
{
    public Weapon()
    {
        Type = ItemType.Weapon;
    }

    public Weapon(string name, string desc, int worth) 
        : base(name, desc, worth, 1)
    {
        Type = ItemType.Weapon;
    }

    /// <summary>
    /// Amount of damage we apply to target
    /// </summary>
    public int Damage { get; set; }

    public Character Owner { get; set; }

    /// <summary>
    /// Attack with this weapon
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// Think gets updated on every frame the item is equipped
    /// </summary>
    public virtual void Think() { }
    public virtual void OnEquip() { Debug.LogFormat("Equipped '{0}'", Name); }
    public virtual void OnDequip()
    {
        Debug.LogFormat("Dequip '{0}'", Name);
        Owner = null;
    }

}

