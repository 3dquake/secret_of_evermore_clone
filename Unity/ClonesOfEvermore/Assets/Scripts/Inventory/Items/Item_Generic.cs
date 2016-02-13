using System;
using UnityEngine;

/// <summary>
/// Generic item class
/// </summary>
public class Item_Generic : Item
{
    /// <summary>
    /// Empty generic
    /// </summary>
    public Item_Generic()
        : base()
    {
    
    }

    public Item_Generic(string name, string desc, int worth, int amount) 
        : base(name, desc, worth, amount)
    {
        
    }

    public override object Clone()
    {
        return new Item_Generic(Name, Description, Worth, Amount);
    }
}