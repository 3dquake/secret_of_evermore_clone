using UnityEngine;
using System.Collections;

public class Item {

    public Item(int amount)
    { 
        Amount = amount;
        Type = ItemType.Generic;
    }

    public ItemType Type
    {
        get;
        protected set;
    }
    public enum ItemType
    {
        Weapon, Armor, Generic
    }

    public string Name
    {
        get; set;
    }
    public int Amount
    {
        get; set;
    }

    public Item Clone()
    {
        return new Item(Amount);
    }


}
