using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : EquipmentSlot
{

    private void Update()
    {
        if (transform.childCount == 0 && IsFull)
        {
            DeleteItem();
        }
        if (transform.childCount == 1 && !IsFull)
        {
            Transform DraggedItem = this.transform.GetChild(0);
            AddItem(DraggedItem.GetComponent<DraggableItem>().Item);
        }
    }

    public override void AddItem(Item item)
    {
        base.AddItem(item);

    }

    public void AddExistingItem(int quantity)
    {
        Item.Quantity += quantity;
    }

    public bool UseItem()
    {
        if(InventoryManager.UseItem(this.Item))
        {
            return true;
        }
        return false;
    }

}
