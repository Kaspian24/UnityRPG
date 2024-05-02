using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject InventoryMenu;

    [SerializeField]
    private GameObject EquipmentMenu;

    [SerializeField]
    private GameObject InventoryPanel;

    [SerializeField]
    private Camera uiCamera;

    [SerializeField]
    private InventorySlot[] inventorySlots;

    [SerializeField]
    private EquipmentSlot[] equipmentSlots;

    [SerializeField]
    private PlayerSlot[] playerSlots;

    [SerializeField]
    private GameObject scrollPanel;

    [SerializeField]
    private GameObject slotPrefab;

    [Header("STATS")]
    [SerializeField]
    private int HP;
    [SerializeField]
    private int MaxHP;
    [SerializeField]
    private int MP;
    [SerializeField]
    private int MaxMP;
    [SerializeField]
    private int STR;
    [SerializeField]
    private int MAG;
    [SerializeField]
    private int DEF;


    [Header("UI ELEMENTS")]
    [SerializeField]
    private TextMeshProUGUI HpText; 
    [SerializeField]
    private TextMeshProUGUI MpText;
    [SerializeField]
    private TextMeshProUGUI StrText;
    [SerializeField]
    private TextMeshProUGUI MagText;
    [SerializeField]
    private TextMeshProUGUI DefText;

    [SerializeField]
    private GameObject descriptionPanel;
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private TextMeshProUGUI itemDescription;

    private bool inventoryOpen = false;

    void Start()
    {
        GameObject item = GameObject.FindGameObjectWithTag("Item");
        Item i = item.GetComponent<Item>();
        AddItem(i);
        Destroy(item);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && inventoryOpen)
        {
            Time.timeScale = 1;
            descriptionPanel.SetActive(false);
            InventoryPanel.SetActive(false);
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            uiCamera.enabled = false;
            inventoryOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !inventoryOpen)
        {
            Time.timeScale = 0;
            InventoryPanel.SetActive(true);
            InventoryMenu.SetActive(true);
            uiCamera.enabled = true;
            inventoryOpen = true;
        }
    }

    public void AddItem(Item item)
    {

        if (item.ItemType == ItemType.Consumable)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (!inventorySlots[i].IsFull)
                {

                    GameObject draggableItem = new GameObject("DraggableItem");
                    draggableItem.AddComponent<Image>();
                    draggableItem.AddComponent<DraggableItem>();
                    draggableItem.AddComponent<Item>();
                    draggableItem.GetComponent<Item>().copyItem(item);
                    draggableItem.GetComponent<DraggableItem>().Item = draggableItem.GetComponent<Item>();
                    draggableItem.GetComponent<Image>().sprite = item.Sprite;
                    draggableItem.GetComponent<DraggableItem>().parentAfterDrag = inventorySlots[i].transform;
                    draggableItem.GetComponent<DraggableItem>().Image = draggableItem.GetComponent<Image>();

                    draggableItem.transform.SetParent(inventorySlots[i].transform);

                    inventorySlots[i].AddItem(draggableItem.GetComponent<Item>());
                    break;
                }
                if (inventorySlots[i].Item.ItemId == item.ItemId)
                {
                    inventorySlots[i].AddExistingItem(item.Quantity);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (!equipmentSlots[i].IsFull)
                {
                    GameObject draggableItem = new GameObject("DraggableItem");
                    draggableItem.AddComponent<Image>();
                    draggableItem.AddComponent<DraggableItem>();
                    draggableItem.AddComponent<Item>();
                    draggableItem.GetComponent<Item>().copyItem(item);
                    draggableItem.GetComponent<DraggableItem>().Item = draggableItem.GetComponent<Item>();
                    draggableItem.GetComponent<Image>().sprite = item.Sprite;
                    draggableItem.GetComponent<DraggableItem>().parentAfterDrag = equipmentSlots[i].transform;
                    draggableItem.GetComponent<DraggableItem>().Image = draggableItem.GetComponent<Image>();

                    draggableItem.transform.SetParent(equipmentSlots[i].transform);

                    equipmentSlots[i].AddItem(draggableItem.GetComponent<Item>());
                    break;
                }
            }
        }
    }

    public bool UseItem(Item item)
    {
        if (HP < MaxHP)
        {
            if ((HP + item.HP) <= MaxHP)
            {
                HP += item.HP;
            }
            else
            {
                HP = MaxHP;
            }
            UpdateStats();
            return true;
        }
        else
        {
            return false;
        }

    }

    public void UpdateStats()
    {
        HpText.text = "HP - " + HP.ToString() + "/" + MaxHP.ToString();
        MpText.text = "MP - " + MP.ToString() + "/" + MaxMP.ToString();
        StrText.text = "STR - " + STR.ToString();
        MagText.text = "MAG - " + MAG.ToString();
        DefText.text = "DEF - " + DEF.ToString();
    }

    public void UpdateStats(Item item, bool equipped)
    {
        if(equipped)
        {
            MaxHP += item.HP;
            MaxMP += item.MP;
            STR += item.STR;
            MAG += item.MAG;
            DEF += item.DEF;
        }
        else if(!equipped)
        {
            MaxHP -= item.HP;
            MaxMP -= item.MP;
            STR -= item.STR;
            MAG -= item.MAG;
            DEF -= item.DEF;
        }

        HpText.text = "HP - " + HP.ToString() + "/" + MaxHP.ToString();
        MpText.text = "MP - " + MP.ToString() + "/" + MaxMP.ToString();
        StrText.text = "STR - " + STR.ToString();
        MagText.text = "MAG - " + MAG.ToString();
        DefText.text = "DEF - " + DEF.ToString();


    }

    public void openInventory()
    {
        InventoryMenu.SetActive(true);
        EquipmentMenu.SetActive(false);
    }

    public void openEquipment()
    {
        InventoryMenu.SetActive(false);
        EquipmentMenu.SetActive(true);
    }

    public void showDescription(Item item)
    {
        if (item != null)
        {
            descriptionPanel.SetActive(true);
            itemName.text = item.ItemName;
            if (item.ItemType == ItemType.Consumable)
            {
                itemDescription.text = item.Description + "\n\nQuantity: " + item.Quantity;
                
            }
            else
            {
                itemDescription.text = item.Description + "\n" + item.PrintStats();
            }
            itemName.enabled = true;
            itemDescription.enabled = true;

        }
        else
        {
            descriptionPanel.SetActive(false);
            itemName.enabled = false;
            itemDescription.enabled = false;
        }
        
        
    }

    public void AddSlots()
    {
        RectTransform rectTransform = scrollPanel.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, rectTransform.offsetMin.y - 110);
        for (int i = 0; i < 7; i++)
        {
            Instantiate(slotPrefab, scrollPanel.transform);
        }
    }
    

}
