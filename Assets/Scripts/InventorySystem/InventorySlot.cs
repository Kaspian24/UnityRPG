using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Special type of inventory slot for managing consumable and key items
/// </summary>
public class InventorySlot : EquipmentSlot
{
    /// <summary>
    /// Deletes or adds and object if dragged to or away but not added to another slot
    /// </summary>
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

    /// <inheritdoc/>
    public override void AddItem(Item item)
    {
        base.AddItem(item);

    }

    /// <summary>
    /// Increases quantity of the item in slot
    /// </summary>
    /// <param name="quantity"></param>
    public void AddExistingItem(int quantity)
    {
        Item.Quantity += quantity;
    }

    /// <summary>
    /// Uses the item in slot
    /// </summary>
    /// <returns>True if item was used. False otherwise.</returns>
    public bool UseItem()
    {
        if(InventoryManager.UseItem(this.Item))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets the reference to the item when it's put in the slot. Items can be stacked when they have the same id
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem;
        if (draggableItem = dropped.GetComponent<DraggableItem>())
        {
            if (!IsFull)
            {
            
                if (!CheckTypeList(draggableItem.Item))
                {
                    return;
                }
                draggableItem.parentAfterDrag = transform;
                AddItem(draggableItem.Item);
            }
            else
            {
                if (draggableItem.Item.ItemId == this.Item.ItemId)
                {
                    AddExistingItem(draggableItem.Item.Quantity);
                    Destroy(dropped);
                }
            }
        }
        
    }


}
