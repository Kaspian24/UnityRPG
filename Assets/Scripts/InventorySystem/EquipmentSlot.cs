using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class that manages equipment slots in equipment tab
/// </summary>
public class EquipmentSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField]
    private Item item;

    [SerializeField]
    private bool isFull;

    [SerializeField]
    private GameObject itemPrefab;

    private InventoryManager inventoryManager;

    [SerializeField]
    private ItemType[] typeList;

    public bool IsFull { get => isFull; set => isFull = value; }
    public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    public InventoryManager InventoryManager { get => inventoryManager; set => inventoryManager = value; }
    public Item Item { get => item; set => item = value; }
    public ItemType[] TypeList { get => typeList; set => typeList = value; }

    /// <summary>
    /// Finds the inventory manager on start
    /// </summary>
    private void Start()
    {
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<InventoryManager>();
    }

    /// <summary>
    /// Deletes or adds and object if dragged to or away but not added to another slot
    /// </summary>
    private void Update()
    {
        if(transform.childCount == 0 && IsFull)
        {
            DeleteItem();
        }
        if (transform.childCount == 1 && !IsFull)
        {
            Transform DraggedItem = this.transform.GetChild(0);
            AddItem(DraggedItem.GetComponent<DraggableItem>().Item);
        }
    }

    /// <summary>
    /// Adds an item reference to the slot
    /// </summary>
    /// <param name="item"></param>
    public virtual void AddItem(Item item)
    {
        this.item = item;
        IsFull = true;

        ItemPrefab = Resources.Load("Prefabs/Items/" + item.ItemId) as GameObject;

    }

    /// <summary>
    /// Deletes item reference from the slot
    /// </summary>
    public virtual void DeleteItem()
    {
        this.isFull = false;
        this.item = null;
        this.itemPrefab = null;
    }

    /// <summary>
    /// Gets the reference to item when it's put in the slot
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (!isFull)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem;
            if(draggableItem = dropped.GetComponent<DraggableItem>())
            {
                if(!CheckTypeList(draggableItem.Item))
                {
                    return;
                }
                draggableItem.parentAfterDrag = transform;
                AddItem(draggableItem.Item);
            }
            
        }
    }

    /// <summary>
    /// Drops item on the ground
    /// </summary>
    public virtual void DropItem()
    {
        //itemPrefab.GetComponent<DraggableItem>().Item.copyItem(this.Item);
        Vector3 playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Instantiate(ItemPrefab, playerTransform, Quaternion.identity);
        if (Item.Quantity < 2)
        {
            DeleteItem();
        }
    }

    /// <summary>
    /// Shows description of the item
    /// </summary>
    /// <param name="item"></param>
    public void ShowDescription(Item item)
    {
        InventoryManager.showDescription(item);
    }

    /// <summary>
    /// Closes the description window
    /// </summary>
    public void CloseDescription()
    {
        InventoryManager.CloseDescription();
    }

    /// <summary>
    /// Checks if the item can be put in the slot
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CheckTypeList(Item item)
    {
        foreach(ItemType type in typeList)
        {
            if(item.ItemType == type)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Manages mouse clicks
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        CloseDescription();
    }
}
