using UnityEngine;
using System.Collections.Generic;

public class Inventory {

    #region Events
    /// <summary>
    /// This event will be raised when an item is removed
    /// </summary>
    public event InventoryEvent onRemove;

    /// <summary>
    /// This event will be raised when an item is added
    /// </summary>
    public event InventoryEvent onAdd;

    /// <summary>
    /// This event will be raised when an item is added or removed
    /// </summary>
    public event InventoryEvent onChange;

    public delegate void InventoryEvent();
    #endregion

    /// <summary>
    /// Maximum carry size
    /// </summary>
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
    /// <summary>
    /// Count of actual items in inventory
    /// </summary>
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
    /// <summary>
    /// Currently selected item
    /// </summary>
    public Item Selected
    {
        get;set;
    }

    // List of inventory items
    List<Item> m_items;
    public ItemFactory Factory
    {
        get
        {
            return m_factory;
        }
    }
    ItemFactory m_factory;

    public Inventory(int capacity = 8)
    {
        m_items = new List<Item>(capacity);

        m_factory = new ItemFactory();

        // Initialize item prototypes
        Item[] items = 
        {
            // Generics
            new Item_Generic(),

            // Weapons
            new Weapon_Sword(),
            new Weapon_Melee(),
            new Weapon_Staff(),
            new Weapon_Bow(),

            // Armor
            new Armor_Chestplate(),
        };

        m_factory.AddRange(items);

    }

    /// <summary>
    /// Gives the item but doesn't add to the inventory. Perfectly fine for NPC's and one-item characters
    /// </summary>
    public Item Give(VisualItem item)
    {
        //if (item == null)
        VisualItem clone = GameObject.Instantiate(item);
        clone.Link = (Item)m_factory.Create(item);
        clone.gameObject.SetActive(false);
        return clone.Link;
    }

    /// <summary>
    /// Add an item to the inventory.
    /// If the item already exists, we add more items to that stack. Unless it's unique
    /// </summary>
    /// <param name="item">The item to add</param>
    public bool Add(VisualItem item)
    {
        // Lets not add anything we can't add
        if (item == null)
            return false;

        if (item.Link == null)
            item.Link = (Item)m_factory.Create(item);
        
        // Inventory is full, refuse to add any more
        if (m_items.Count == m_items.Capacity)
            return false;

        // Try to find the item from the inventory
        Item match = m_items.Find(x => x.Type == item.Link.Type && x.Name == item.niceName);

        // If the item is 'unique' (weapon, armor), it will not be stacked

        // Not found; lets add it
        if (match == null || item.Link.Type != Item.ItemType.Generic)
        {
            m_items.Add(item.Link);
        }
        // Match found; Lets increase the amount of the match
        else
        {
            //FIXME: references?
            match.Amount += item.Link.Amount;
            GameObject.Destroy(item.gameObject);
        }

        // Raise events that we added some items
        if (onAdd != null)
            onAdd();

        if (onChange != null)
            onChange();

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

            // Clone the match
            Item temp = (Item)m_factory.Create(match.Link);
            temp.Link = GameObject.Instantiate<VisualItem>(match.Link);
            temp.Amount = amount; // Set the amount of items we just removed
            match = temp;
            //Item temp = (Item)match.Clone();
            //temp.Amount = amount;
            //match = temp;
        }

        // Raise events that we dropped some items
        if (onRemove != null)
            onRemove();

        if (onChange != null)
            onChange();

        return match;
    }

    /// <summary>
    /// Check if player has this kind of item
    /// </summary>
    /// <param name="match">Item to search</param>
    /// <returns>True if found</returns>
    public bool HasItem(System.Predicate<Item> match)
    {
        foreach (Item item in m_items)
        {
            if (match(item))
                return true;
        }
        return false;
    }

}
