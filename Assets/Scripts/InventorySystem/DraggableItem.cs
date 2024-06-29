using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

/// <summary>
/// Manages the behaviour of draggable item object in inventory
/// </summary>
[System.Serializable]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    public Transform parentAfterDrag;
    [SerializeField]
    private Image image;

    [SerializeField]
    private Item item;

    public Item Item { get => item; set => item = value; }
    public Image Image { get => image; set => image = value; }

    /// <summary>
    /// Sets current slot as parent
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Image.raycastTarget = false;
    }

    /// <summary>
    /// Object follows mouse cursor
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// Sets new slot as parent
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        Image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    /// <summary>
    /// Manages mouse button clicks
    /// </summary>
    /// <param name="eventData"></param>
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

    /// <summary>
    /// Opens description window with name and description of item
    /// </summary>
    private void OnLeftCLick()
    {
        EquipmentSlot slot;
        if (slot = parentAfterDrag.GetComponent<EquipmentSlot>())
        {
            slot.ShowDescription(this.Item);
        }
    }

    /// <summary>
    /// Uses item
    /// </summary>
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

    /// <summary>
    /// Drops item on the ground
    /// </summary>
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

}
