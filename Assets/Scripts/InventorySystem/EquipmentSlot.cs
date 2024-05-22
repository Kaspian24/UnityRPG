using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField]
    private Item item;

    [SerializeField]
    private bool isFull;

    [SerializeField]
    private GameObject itemPrefab;

    private InventoryManager inventoryManager;

    public bool IsFull { get => isFull; set => isFull = value; }
    public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    public InventoryManager InventoryManager { get => inventoryManager; set => inventoryManager = value; }
    public Item Item { get => item; set => item = value; }

    private void Start()
    {
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<InventoryManager>();
    }

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

    public virtual void AddItem(Item item)
    {
        this.item = item;
        IsFull = true;

        ItemPrefab = Resources.Load("Prefabs/Items/" + item.ItemId) as GameObject;

    }

    public virtual void DeleteItem()
    {
        this.isFull = false;
        this.item = null;
        this.itemPrefab = null;
    }

    public void SwapItems(Item item)
    {
        (this.item, item) = (item, this.item);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (!isFull)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem;
            if(draggableItem = dropped.GetComponent<DraggableItem>())
            {
                draggableItem.parentAfterDrag = transform;
                AddItem(draggableItem.Item);
            }
            
        }
    }

    public void DropItem()
    {
        itemPrefab.GetComponent<Item>().copyItem(this.Item);
        Vector3 playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Instantiate(ItemPrefab, playerTransform, Quaternion.identity);
        DeleteItem();
    }

    public void ShowDescription(Item item)
    {
        InventoryManager.showDescription(item);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.showDescription(null);
    }

}
