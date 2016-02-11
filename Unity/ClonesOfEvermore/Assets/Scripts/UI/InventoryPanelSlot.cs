using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable), typeof(Image))]
public class InventoryPanelSlot : MonoBehaviour, IPointerClickHandler
{
    public Image Icon
    {
        get
        {
            if (!m_icon)
                m_icon = transform.GetChild(0).GetComponent<Image>();
            return m_icon;
        }
    }
    Image m_icon;

    public Selectable Input
    {
        get
        {
            if (!m_input)
                m_input = GetComponent<Selectable>();
            return m_input;
        }
    }
    Selectable m_input;

    public Item Item
    {
        get; set;
    }



    void OnEnable()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.Inventory.Selected = Item;
    }
}

