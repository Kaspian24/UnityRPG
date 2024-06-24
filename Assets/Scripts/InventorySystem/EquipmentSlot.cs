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

    [SerializeField]
    private ItemType[] typeList;

    public bool IsFull { get => isFull; set => isFull = value; }
    public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    public InventoryManager InventoryManager { get => inventoryManager; set => inventoryManager = value; }
    public Item Item { get => item; set => item = value; }
    public ItemType[] TypeList { get => typeList; set => typeList = value; }

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

    public void DropItem()
    {
        //itemPrefab.GetComponent<DraggableItem>().Item.copyItem(this.Item);
        Vector3 playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Instantiate(ItemPrefab, playerTransform, Quaternion.identity);
        if (Item.Quantity < 2)
        {
            DeleteItem();
        }
    }

    public void EquipItem()
    {
        Transform weaponPoint = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<Transform>();

        // Utwórz instancjê prefabrykatu
         GameObject weaponSlot = Instantiate(ItemPrefab);

        // Ustaw rodzica dla nowo utworzonej instancji
        weaponSlot.transform.SetParent(weaponPoint);

        // Opcjonalnie zresetuj lokaln¹ pozycjê, rotacjê i skalowanie nowego obiektu
        weaponSlot.transform.localPosition = Vector3.zero;
        weaponSlot.transform.localRotation = Quaternion.Euler(Vector3.zero);
        weaponSlot.transform.localScale = Vector3.one;
        weaponSlot.GetComponent<Rigidbody>().isKinematic = true;
        weaponSlot.GetComponent<Rigidbody>().detectCollisions = false;
        weaponSlot.GetComponent<Collider>().enabled = false;
    }

    public void UnequipItem()
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("Sword");
        Destroy(weapon);
    }

    public void ShowDescription(Item item)
    {
        InventoryManager.showDescription(item);
    }

    public void CloseDescription()
    {
        InventoryManager.CloseDescription();
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        CloseDescription();
    }
}
