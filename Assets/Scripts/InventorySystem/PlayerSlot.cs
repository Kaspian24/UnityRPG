using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSlot : EquipmentSlot, IPointerClickHandler
{

    public void EquipItem(Item item)
    {

        this.ItemId = item.ItemId;
        this.ItemName = item.ItemName;
        this.Sprite = item.Sprite;
        IsFull = true;

        ItemImage.sprite = Sprite;
        ItemImage.enabled = true;

        swapStats(item);

    }

    public new void DeleteItem()
    {
        this.ItemName = "";
        this.Quantity = 0;
        this.Sprite = null;
        IsFull = false;

        ItemImage.sprite = null;
        ItemImage.enabled = false;

    }

    public new void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        InventoryManager.DeselectAllSlots();
        SelectedShader.SetActive(true);
        IsSelected = true;
    }

    private void OnRightClick()
    {
        if (IsFull)
        {
            InventoryManager.UpdateStats(this, false);
            InventoryManager.AddItem(this);
            DeleteItem();

        }
    }
}
