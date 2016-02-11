using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanel : EvermorePanel
{

    public Sprite emptyIcon;
    Item[] m_items;

    Item m_selected;

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

    public void SetItem(int slot, Item item)
    {
        //Debug.LogFormat("Changed slot '{0}' sprite '{1}' to '{2}'", slots[slot].name, slots[slot].Icon.sprite.name, icon.name);
        slots[slot].Icon.sprite = item.Link.icon;
        slots[slot].Item = item;
    }

    public void RemoveItem(Item item, int amount = 1)
    {
        Item temp = GameManager.Instance.Inventory.Remove(item, amount);
        if (temp == null)
            return;

        ClearPreview();

        item.Link.SetActive(true);
        item.Link.transform.position = GameManager.Instance.Characters.Selected.Link.Feet;
    }

    void UpdateInventory()
    {
        

        if (m_items == GameManager.Instance.Inventory.Items)
            return;

        m_items = GameManager.Instance.Inventory.Items;

        for (int i = 0; i < m_items.Length; i++)
        {
            if (m_items[i].Link.icon != null)
                SetItem(i, m_items[i]);
        }
    }

    public override void Refresh()
    {
        if (m_selected != GameManager.Instance.Inventory.Selected)
        {
            m_selected = GameManager.Instance.Inventory.Selected;
            SetPreview(m_selected);
        }
        else if (m_selected == null && GameManager.Instance.Inventory.Selected == null)
        {
            ClearPreview();
        }

        UpdateInventory();

        if (Input.GetKeyDown(KeyCode.R))
            RemoveItem(m_selected);

    }

    public override void Initialize()
    {
        slots = GetComponentsInChildren<InventoryPanelSlot>();
        //inspectorDrop.onClick.AddListener(DropItem);

    }

    public override void OnHide()
    {
        GameManager.Instance.Inventory.Selected = null;
        m_selected = null;
    }

    //void DropItem()
    //{
    //    RemoveItem();
    //}
    
}

