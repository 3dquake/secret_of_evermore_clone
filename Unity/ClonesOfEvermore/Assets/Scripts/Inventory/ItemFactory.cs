using System;
using System.Collections.Generic;

using UnityEngine;

public class ItemFactory
{

    // Item prototypes
    List<Item> m_itemList;

    public ItemFactory()
    {

    }

    public void AddRange(params Item[] items)
    {
        if (m_itemList == null)
            m_itemList = new List<Item>(items.Length);

        m_itemList.AddRange(items);

    }

    public object Create(VisualItem vitem)
    {
        object clone = null;
        //Item clone = (Item)m_itemList.Find(x => x.Type == vitem.Type && x.GetType().Name == vitem.itemName).Clone();
        //CopyValues(vitem, clone);
        //return clone;
        foreach (Item item in m_itemList)
        {
            if (item.Type == vitem.Type && item.GetType().Name == vitem.itemName)
            {
                //Debug.Log(item.GetType().Name);
                clone = item.Clone();
                CopyValues(vitem, (Item)clone);
                break;
            }

        }
        return clone;
    }

    /// <summary>
    /// Copies values from VisualItem to Item, excluding the link
    /// </summary>
    /// <param name="vitem">VisualItem to copy values from</param>
    /// <param name="item">Item to copy values to</param>
    public void CopyValues(VisualItem vitem, Item item)
    {

        item.Name = vitem.niceName;
        item.Description = vitem.description;
        item.Worth = vitem.worth;
        item.Amount = vitem.amount;
        //item.Link = GameObject.Instantiate<VisualItem>(vitem);
    }


    //public object Create(Type type)
    //{
    //    object clone = null;

    //    foreach (Item item in m_itemList)
    //    {
    //        if (item.GetType() == type)
    //        {
    //            clone = item.Clone();
    //        }
    //    }
    //    return clone;
    //}

}

