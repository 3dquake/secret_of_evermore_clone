using UnityEngine;
using System.Collections.Generic;

public class Inventory {

    public int Capacity
    {
        get
        {
            return (m_items != null) ? m_items.Capacity : 0;
        }
    }
    public int Count
    {
        get
        {
            return (m_items != null) ? m_items.Count : 0;
        }
    }

    List<Item> m_items;

    public Inventory(int capacity = 8)
    {
        m_items = new List<Item>(capacity);
    }

    public void Add(Item item)
    {
        m_items.Add(item);
    }

}
