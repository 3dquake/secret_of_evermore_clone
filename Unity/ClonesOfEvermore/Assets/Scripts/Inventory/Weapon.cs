using System;
using UnityEngine;

public abstract class Weapon : Item
{
    public Weapon(string name, string desc, int worth, int amount) : base(name, desc, worth, amount)
    {
        Type = ItemType.Weapon;
    }

    /// <summary>
    /// Amount of damage we apply to target
    /// </summary>
    public int Damage { get; set; }

    public abstract void Attack();
}

