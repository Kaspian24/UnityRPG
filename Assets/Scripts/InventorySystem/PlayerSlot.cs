using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerSlot : EquipmentSlot
{
    //public GameObject weapon;
    public Transform weaponPoint;

    private void Update()
    {
        if(transform.childCount == 0 && IsFull)
        {
            InventoryManager.UpdateStats(Item, false);
            DeleteItem();
        }
        if (transform.childCount == 1 && !IsFull)
        {
            Transform DraggedItem = this.transform.GetChild(0);
            AddItem(DraggedItem.GetComponent<DraggableItem>().Item);
            InventoryManager.UpdateStats(Item, true);
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (!IsFull)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem;
            if ((draggableItem = dropped.GetComponent<DraggableItem>()) && (CheckTypeList(draggableItem.Item)))
            {
                draggableItem.parentAfterDrag = transform;
                AddItem(draggableItem.Item);
                InventoryManager.UpdateStats(draggableItem.Item, true);
            }
        }
    }
}
