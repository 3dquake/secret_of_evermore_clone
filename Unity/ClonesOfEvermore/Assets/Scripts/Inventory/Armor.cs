using System;
using UnityEngine;

public class Armor : Item
{
    public Armor(string name, string desc, int worth, int amount) : base(name, desc, worth, amount)
    {
        Type = ItemType.Armor;
    }

    public int Defence { get; set; }
}

