using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Transform parentBeforeDrag;
    public Transform parentAfterDrag;
    [SerializeField]
    private Image image;

    [SerializeField]
    private Item item;

    public Item Item { get => item; set => item = value; }
    public Image Image { get => image; set => image = value; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Image.raycastTarget = false;
        parentBeforeDrag = parentAfterDrag;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if(parentAfterDrag.name == "PlayerSlot (4)")
        {
            OnEquip();
        }
        if (parentBeforeDrag.name == "PlayerSlot (4)")
        {
            OnUnequip();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftCLick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
        if(eventData.button == PointerEventData.InputButton.Middle)
        {
            OnMiddleClick();
        }
    }

    private void OnLeftCLick()
    {
        EquipmentSlot slot;
        if (slot = parentAfterDrag.GetComponent<EquipmentSlot>())
        {
            slot.ShowDescription(this.Item);
        }
    }

    private void OnRightClick()
    {
        InventorySlot slot;
        if (slot = parentAfterDrag.GetComponent<InventorySlot>())
        {
            if(slot.UseItem())
            {
                Item.Quantity -= 1;
                slot.ShowDescription(this.Item);
                if (Item.Quantity <= 0)
                {
                    slot.ShowDescription(null);
                    Destroy(this.gameObject);
                }
            }

        }
    }

    private void OnMiddleClick()
    {
        if (this.Item.ItemType == ItemType.KeyItem)
        {
            return;
        }

        EquipmentSlot slot;
        if (slot = parentAfterDrag.GetComponent<EquipmentSlot>())
        {
            slot.DropItem();
            slot.CloseDescription();
            if (Item.Quantity < 2)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Item.Quantity--;
            }
        }
    }

    private void OnEquip()
    {
        if (this.Item.ItemType != ItemType.EquipHand)
        {
            return;
        }

        EquipmentSlot slot;
        if (slot = parentAfterDrag.GetComponent<EquipmentSlot>())
        {
            slot.EquipItem();
            slot.CloseDescription();
        }
    }

    private void OnUnequip()
    {
        if (this.Item.ItemType != ItemType.EquipHand)
        {
            return;
        }

        EquipmentSlot slot;
        if (slot = parentAfterDrag.GetComponent<EquipmentSlot>())
        {
            slot.UnequipItem();
            slot.CloseDescription();
        }
    }

}
