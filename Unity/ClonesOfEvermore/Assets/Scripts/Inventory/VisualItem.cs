using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VisualItem : MonoBehaviour
{
    [Header("Starting values")]
    public string niceName;
    [Tooltip("Item datatype name")]
    public string itemName;
    public string description;
    public int worth, amount;
    public Item.ItemType Type;

    public bool Active
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            gameObject.SetActive(value);
        }
    }

    /// <summary>
    /// Link to the item's data
    /// </summary>
    public Item Link
    {
        get
        {
            return m_link;
        }
        set
        {
            if (m_link == null)
            {
                m_link = value;
                m_link.Link = this;
            }

        }
    }
    Item m_link;

    public Sprite icon;

    //void OnEnable()
    //{
    //    //if (m_link == null)
    //    //    Link = new Item(Name, Description, Worth, Amount);
    //}
}