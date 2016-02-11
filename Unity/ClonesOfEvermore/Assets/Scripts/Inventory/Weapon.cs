using System;
using UnityEngine;

public abstract class Weapon : Item
{
    public Weapon() : base(1)
    {
        Type = ItemType.Weapon;
    }

    /// <summary>
    /// Amount of damage we apply to target
    /// </summary>
    public int Damage { get; set; }

    public abstract void Attack();
}

