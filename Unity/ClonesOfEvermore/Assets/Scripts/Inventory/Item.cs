using UnityEngine;
using System.Collections;

public class Item {

    public Item(string name, string desc, int worth, int amount)
    { 
        Name = name;
        Description = desc;        
        Amount = amount;
        Worth = worth;
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
    public string Description
    {
        get;
        set;
    }
    public int Amount
    {
        get; set;
    }
    public int Worth { get; set; }

    public Item Clone()
    {
        Item clone = new Item(Name, Description, Worth, Amount);
        clone.Link = GameObject.Instantiate<GameObject>(Link.gameObject).GetComponent<VisualItem>();
        return clone;
    }

    public VisualItem Link
    {
        get
        {
            return m_link;
        }
        set
        {
            if (!m_link)
                m_link = value;
        }
    }
    VisualItem m_link;

}
