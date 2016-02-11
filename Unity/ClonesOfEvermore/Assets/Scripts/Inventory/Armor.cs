using System;
using UnityEngine;

public class Armor : Item
{
    public Armor() : base(1)
    {
        Type = ItemType.Armor;
    }

    public int Defence { get; set; }
}

