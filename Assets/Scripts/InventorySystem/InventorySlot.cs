using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : Item, IPointerClickHandler
{

    // Start is called before the first frame update

    private bool isFull;
    private bool isSelected;


    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI quantityText;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private GameObject selectedShader;
    [SerializeField]
    private GameObject itemPrefab;

    private InventoryManager inventoryManager;

    

    public bool IsFull { get => isFull; set => isFull = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }
    public Image ItemImage { get => itemImage; set => itemImage = value; }
    public GameObject SelectedShader { get => selectedShader; set => selectedShader = value; }
    public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    public InventoryManager InventoryManager { get => inventoryManager; set => inventoryManager = value; }

    private void Start()
    {
        InventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void AddItem(Item item)
    {
        this.ItemId = item.ItemId;
        this.ItemName = item.ItemName;
        this.Quantity = item.Quantity;
        this.Sprite = item.Sprite;
        this.ItemType = item.ItemType;
        IsFull = true;

        quantityText.text = Quantity.ToString();
        quantityText.enabled = true;
        ItemImage.sprite = Sprite;
        ItemImage.enabled = true;

        ItemPrefab = Resources.Load("Prefabs/Items/" + ItemId) as GameObject;

        swapStats(item);

    }

    public void AddExistingItem(int quantity)
    {
        this.Quantity += quantity;
        quantityText.enabled = true;
        quantityText.text = Quantity.ToString();
    }

    public void DeleteItem()
    {
        if (Quantity > 1)
        {
            this.Quantity--;
            quantityText.text = Quantity.ToString();
            
        }
        else if (Quantity == 1)
        {
            this.ItemType = ItemType.None;
            this.ItemId = 0;
            this.ItemName = "";
            this.Quantity = 0;
            this.Sprite = null;
            IsFull = false;

            quantityText.enabled = false;
            ItemImage.sprite = null;
            ItemImage.enabled = false;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
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
            //Vector3 playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
            //Instantiate(ItemPrefab, playerTransform, Quaternion.identity);
            if (this.ItemType == ItemType.Consumable && InventoryManager.UseItem(this))
            {
                DeleteItem();
            }
        }
    }

}
