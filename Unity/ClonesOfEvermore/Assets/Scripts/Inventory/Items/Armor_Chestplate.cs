using System;
using UnityEngine;

public class Armor_Chestplate : Armor
{
    public Armor_Chestplate() 
        : base()
    {

    }

    public Armor_Chestplate(string name, string desc, int worth) : base(name, desc, worth)
    {
        Type = ItemType.Armor;
    }

    public override void OnEquip()
    {
        Defence = 5;
    }

    public override object Clone()
    {
        return MemberwiseClone();
    }
}

