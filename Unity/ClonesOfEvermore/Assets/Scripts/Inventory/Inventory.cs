using UnityEngine;
using System.Collections.Generic;

public class Inventory {

    /// <summary>
    /// This event will be raised when an item is removed
    /// </summary>
    public InventoryEvent OnRemove;

    /// <summary>
    /// This event will be raised when an item is added
    /// </summary>
    public InventoryEvent OnAdd;

    /// <summary>
    /// This event will be raised when an item is added or removed
    /// </summary>
    public InventoryEvent OnChange;

    public delegate void InventoryEvent();

    public int Capacity
    {
        get
        {
            return (m_items != null) ? m_items.Capacity : 0;
        }
        set
        {
            if (m_items != null)
                m_items.Capacity = value;
        }
    }
    public int Count
    {
        get
        {
            return (m_items != null) ? m_items.Count : 0;
        }
    }

    /// <summary>
    /// Array of items currently in the inventory
    /// </summary>
    public Item[] Items
    {
        get
        {
            return m_items.ToArray();
        }
    }
    List<Item> m_items;

    public Item Selected
    {
        get;set;
    }

    public Inventory(int capacity = 8)
    {
        m_items = new List<Item>(capacity);
    }

    /// <summary>
    /// Add an item to the inventory.
    /// If the item already exists, we add more items to that stack.
    /// </summary>
    /// <param name="item">The item to add</param>
    //public void Add(Item item)
    //{
    //    Item match = m_items.Find(x => x.Type == item.Type && x.Equals(item));

    //    // Check if the item exists
    //    if (match == null)
    //    {
    //        m_items.Add(item);
    //    }
    //    else
    //    {
    //        match.Amount += item.Amount;
    //    }
    //}
    public bool Add(VisualItem item)
    {
        // Lets not add anything we can't add
        if (item.Link == null || item == null)
            return false;

        Debug.Log(item.Link);

        // Find the nearest match
        Item match = m_items.Find(x => x.Type == item.Link.Type && x.Name == item.Name);

        // Not found; lets add it
        if (match == null)
        {
            m_items.Add(item.Link);
        }
        // Match found; Lets increase the amount of the match
        else
        {
            match.Amount += item.Link.Amount;
            // We will get rid of the gameObject for now
            // FIXME: References in a stack for each item?
            //GameObject.Destroy(match.Link);
        }

        // Raise events that we added some items
        if (OnAdd != null)
            OnAdd();

        if (OnChange != null)
            OnChange();
        return true;

    }

    /// <summary>
    /// Remove an item from the inventory.
    /// If the removable amount exceeds the amount we have in the inventory, we return that as a whole.
    /// Else, we clone that item and return it instead.
    /// </summary>
    /// <param name="item">Item to remove</param>
    /// <param name="amount">Number of items to remove</param>
    /// <returns>The removed item</returns>
    public Item Remove(Item item, int amount)
    {
        // Don't do anything if it doesn't even exist
        if (item == null)
            return null;

        Item match = m_items.Find(x => x.Type == item.Type && x.Name == item.Name);

        // No match ; no drops
        if (match == null)
            return null;

        // The removable amount is higher or equal as what we got in the inventory right now
        if (match.Amount <= amount)
        {
            // Remove them all as whole
            // At this point the item retains it's "stack"
            m_items.Remove(match);
            //return match;
        }
        // The removable amount is smaller than what we have in our inventory
        else if (match.Amount > amount)
        {
            // Decrease amount from the original first
            match.Amount -= amount;

            // Clone it, if needed?
            Item temp = match.Clone();
            temp.Amount = amount;
            match = temp;
        }

        // Raise events that we dropped some items
        if (OnRemove != null)
            OnRemove();

        if (OnChange != null)
            OnChange();

        return match;
    }

}
