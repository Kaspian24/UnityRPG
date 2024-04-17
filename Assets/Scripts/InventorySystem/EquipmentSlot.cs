using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EquipmentSlot : InventorySlot, IPointerClickHandler
{
    // Start is called before the first frame update

    public new void AddItem(Item item)
    {
        this.ItemId = item.ItemId;
        this.ItemName = item.ItemName;
        this.Sprite = item.Sprite;
        this.ItemType = item.ItemType;
        IsFull = true;

        ItemImage.sprite = Sprite;
        ItemImage.enabled = true;


        ItemPrefab = Resources.Load("Prefabs/Items/" + ItemId) as GameObject;

        swapStats(item);
    }

    public new void DeleteItem()
    {
        this.ItemType = ItemType.None;
        this.ItemId = 0;
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
            if (InventoryManager.EquipItem(this))
            {
                DeleteItem();
            }
            //    Vector3 playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            //    Instantiate(itemPrefab, playerTransform, Quaternion.identity);
            //    DeleteItem();
        }
    }
}
