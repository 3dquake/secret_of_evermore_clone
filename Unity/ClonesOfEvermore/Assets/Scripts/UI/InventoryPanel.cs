using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanel : EvermorePanel
{

    public Sprite emptyIcon;

    // We only need a reference of the inventory
    Item[] m_items;
    Item m_selected;

    // Array of slots in UI
    public InventoryPanelSlot[] slots;

    public Text inspectorTitle;
    public Text inspectorDescription;
    public Image inspectorPreview;

    public Button inspectorDrop;

    public void SetPreview(Item item)
    {
        inspectorTitle.text = item.Name;
        inspectorDescription.text = item.Description;
        inspectorPreview.sprite = item.Link.icon;
    }

    public void ClearPreview()
    {
        inspectorTitle.text = "";
        inspectorDescription.text = "";
        inspectorPreview.sprite = emptyIcon;
    }

    public void ClearSlots()
    {
        foreach (InventoryPanelSlot slot in slots)
        {
            slot.Icon.sprite = emptyIcon;
            slot.Item =  null;
            slot.amount.text = "";
        }
    }

    public void UpdateSlots()
    {

        ClearSlots();

        for (int i = 0; i < m_items.Length; i++)
        {
            slots[i].Icon.sprite = m_items[i].Link.icon;
            slots[i].Item = m_items[i];
            slots[i].amount.text = m_items[i].Amount.ToString();
            //if (m_items[i].Link.icon != null)
            //    UpdateSlots(i, m_items[i]);
            //else
            //    UpdateSlots(i, null);
        }
        //Debug.LogFormat("Changed slot '{0}' sprite '{1}' to '{2}'", slots[slot].name, slots[slot].Icon.sprite.name, icon.name);
        
    }

    public void RemoveItem(Item item, int amount = 1)
    {
        // Catch the removed item
        Item temp = GameManager.Instance.Inventory.Remove(item, amount);

        // No item; abort
        if (temp == null)
            return;

        ClearPreview();
        ClearSelection();
        UpdateSlots();

        temp.Link.Active = true;
        temp.Link.transform.position = GameManager.Instance.Characters.Selected.Link.feet;
    }

    // Updates inventory when Inventory raises an event about it
    void UpdateInventory()
    {
        //// Don't update if the inventories are identical
        //if (m_items == GameManager.Instance.Inventory.Items)
        //    return;

        //Debug.Log("Update Inventory");

        m_items = GameManager.Instance.Inventory.Items;

        UpdateSlots();

        //foreach (Item item in m_items)
        //{
        //    Debug.LogFormat("{0} x {1}", item.Name, item.Amount);
        //}



    }

    void UpdateInspector()
    {
        if (m_selected != GameManager.Instance.Inventory.Selected)
        {
            m_selected = GameManager.Instance.Inventory.Selected;

            if (m_selected == null)
                ClearPreview();
            else
                SetPreview(m_selected);
        }

    }

    public override void Refresh()
    {
        UpdateInspector();

        //UpdateInventory();

        if (Input.GetKeyDown(KeyCode.R))
            RemoveItem(m_selected);

    }

    public override void Initialize()
    {
        slots = GetComponentsInChildren<InventoryPanelSlot>();
        //inspectorDrop.onClick.AddListener(DropItem);

        inspectorDrop.onClick.AddListener(DropSelectedItem);

        GameManager.Instance.Inventory.OnChange += UpdateInventory;

    }

    public void ClearSelection()
    {
        GameManager.Instance.Inventory.Selected = null;
        m_selected = null;
    }

    public override void OnHide()
    {
        ClearSelection();
    }

    public void DropSelectedItem()
    {
        RemoveItem(m_selected);
    }

}

