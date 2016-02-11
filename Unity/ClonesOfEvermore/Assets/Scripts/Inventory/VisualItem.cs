using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VisualItem : MonoBehaviour
{
    [Header("Starting values")]
    public string Name, Description;
    public int Worth, Amount;
    
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

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    void OnEnable()
    {
        if (m_link == null)
            Link = new Item(Name, Description, Worth, Amount);
    }
}