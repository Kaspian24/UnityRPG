using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update

    [Header("DATA")]
    public int itemId;
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    [SerializeField]
    private TextMeshProUGUI quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool isSelected;

    private InventoryManager inventoryManager;

    [SerializeField]
    private GameObject itemPrefab;

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    public void AddItem(int itemId, string itemName, int quantity, Sprite itemSprite)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;

        itemPrefab = Resources.Load("Prefabs/Items/" + itemId) as GameObject;

    }

    public void DeleteItem()
    {
        this.itemName = "";
        this.quantity = 0;
        this.itemSprite = null;
        isFull = false;

        quantityText.enabled = false;
        itemImage.sprite = null;
        itemImage.enabled = false;

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
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        isSelected = true;
    }

    private void OnRightClick()
    {
        Vector3 playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Instantiate(itemPrefab, playerTransform, Quaternion.identity);
        DeleteItem();
    }

}
