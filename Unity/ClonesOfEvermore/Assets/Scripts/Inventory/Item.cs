using UnityEngine;

using System;
using System.Collections;

public abstract class Item : ICloneable {

    /// <summary>
    /// Creates an empty item
    /// </summary>
    public Item(/*string name, string desc, int worth, int amount*/)
    { 
        Name = "";
        Description = "";        
        Amount = 1;
        Worth = 0;
        Type = ItemType.Generic;
    }

    public Item(string name, string desc, int worth, int amount)
    {
        Name = name;
        Description = desc;
        Amount = amount;
        Worth = worth;
        Type = ItemType.Generic;
    }

    /// <summary>
    /// Type of this item
    /// </summary>
    public ItemType Type
    {
        get;
        protected set;
    }
    public enum ItemType
    {
        Weapon, Armor, Generic, Unique
    }

    /// <summary>
    /// Item name
    /// </summary>
    public string Name
    {
        get; set;
    }
    /// <summary>
    /// Item description
    /// </summary>
    public string Description
    {
        get;
        set;
    }
    /// <summary>
    /// Amount of items
    /// </summary>
    public int Amount
    {
        get; set;
    }
    /// <summary>
    /// Item worth
    /// </summary>
    public int Worth { get; set; }
    /// <summary>
    /// Link to this item's visual representation
    /// </summary>
    public VisualItem Link
    {
        get
        {
            return m_link;
        }
        set
        {
            if (!m_link)
            {
                m_link = value;
                m_link.Link = this;
            }
        }
    }
    VisualItem m_link;

    /// <summary>
    /// Clone this item and it's values (excluding link)
    /// </summary>
    /// <returns>Clone of the original item</returns>
    public abstract object Clone();

}
