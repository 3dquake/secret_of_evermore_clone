using System;
using UnityEngine;

public class GenericItem : Item
{
    public GenericItem(string name, string desc, int worth, int amount) : base(name, desc, worth, amount)
    {
        Type = ItemType.Generic;
    }
}