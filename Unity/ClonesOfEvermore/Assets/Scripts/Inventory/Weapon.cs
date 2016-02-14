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

    //public void Shoot(Projectile projectile)
    //{

    //}

    /// <summary>
    /// Attack with this weapon
    /// </summary>
    public abstract void Attack();
    
    /// <summary>
    /// Think gets updated on every frame the item is equipped
    /// </summary>
    public virtual void Think() { }
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

