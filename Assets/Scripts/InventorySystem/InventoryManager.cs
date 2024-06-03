using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
    private List<EquipmentSlot> equipmentSlots;

    [SerializeField]
    private PlayerSlot[] playerSlots;

    [SerializeField]
    private GameObject scrollPanel;

    [SerializeField]
    private GameObject descriptionScrollPanel;

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

    private bool descriptionOpen = false;

    [SerializeField]
    private GameObject chestPanelPrefab;
    [SerializeField]
    private GameObject chestPanel;

    void Start()
    {
        GameObject item;
        if (item = GameObject.FindGameObjectWithTag("Item"))
        {
            Item i = item.GetComponent<DraggableItem>().Item;
            AddItem(i);
            Destroy(item);
        }
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
            //Destroy(chestPanel);
            inventoryOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !inventoryOpen)
        {
            Time.timeScale = 0;
            InventoryPanel.SetActive(true);
            InventoryMenu.SetActive(true);
            //chestPanel = Instantiate(chestPanelPrefab, this.gameObject.transform);
            inventoryOpen = true;
        }
    }

    public void AddItem(Item item)
    {

        if (item.ItemType == ItemType.Consumable || item.ItemType == ItemType.KeyItem)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (!inventorySlots[i].IsFull)
                {

                    GameObject draggableItem = new GameObject("DraggableItem");
                    draggableItem.AddComponent<Image>();
                    draggableItem.AddComponent<DraggableItem>();
                    draggableItem.GetComponent<DraggableItem>().Item = new Item();
                    draggableItem.GetComponent<DraggableItem>().Item.copyItem(item);
                    draggableItem.GetComponent<Image>().sprite = item.Sprite;
                    draggableItem.GetComponent<DraggableItem>().parentAfterDrag = inventorySlots[i].transform;
                    draggableItem.GetComponent<DraggableItem>().Image = draggableItem.GetComponent<Image>();

                    draggableItem.transform.SetParent(inventorySlots[i].transform);

                    inventorySlots[i].AddItem(draggableItem.GetComponent<DraggableItem>().Item);
                    break;
                }
                if (inventorySlots[i].Item.ItemId == item.ItemId && item.ItemType != ItemType.KeyItem)
                {
                    inventorySlots[i].AddExistingItem(item.Quantity);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                if (!equipmentSlots[i].IsFull)
                {
                    GameObject draggableItem = new GameObject("DraggableItem");
                    draggableItem.AddComponent<Image>();
                    draggableItem.AddComponent<DraggableItem>();
                    draggableItem.GetComponent<DraggableItem>().Item = new Item();
                    draggableItem.GetComponent<DraggableItem>().Item.copyItem(item);
                    draggableItem.GetComponent<Image>().sprite = item.Sprite;
                    draggableItem.GetComponent<DraggableItem>().parentAfterDrag = equipmentSlots[i].transform;
                    draggableItem.GetComponent<DraggableItem>().Image = draggableItem.GetComponent<Image>();

                    draggableItem.transform.SetParent(equipmentSlots[i].transform);

                    equipmentSlots[i].AddItem(draggableItem.GetComponent<DraggableItem>().Item);
                    break;
                }
            }
        }
    }

    public bool UseItem(Item item)
    {
        if(item.ItemType == ItemType.KeyItem)
        {
            return false;
        }

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
        CloseDescription();
    }

    public void openEquipment()
    {
        InventoryMenu.SetActive(false);
        EquipmentMenu.SetActive(true);
        CloseDescription();
    }

    public void showDescription(Item item)
    {
        if (item != null)
        {
            descriptionPanel.SetActive(true);
            itemName.text = item.ItemName;
            if (item.ItemType == ItemType.Consumable)
            {
                itemDescription.text = item.Description + "\n\nQuantity: " + item.Quantity + "\n" + item.PrintStats();
                
            }
            else
            {
                itemDescription.text = item.Description + "\n" + item.PrintStats();
            }
            RectTransform descriptionRect = itemDescription.GetComponent<RectTransform>();
            RectTransform panelRect = descriptionScrollPanel.GetComponent<RectTransform>();
            if (descriptionOpen == false)
            {
                
                panelRect.offsetMin = new Vector2(panelRect.offsetMin.x, panelRect.offsetMin.y - descriptionRect.offsetMin.y -100);
            }

            itemName.enabled = true;
            itemDescription.enabled = true;
            descriptionOpen = true;

        }
        else
        {
            descriptionPanel.SetActive(false);
            itemName.enabled = false;
            itemDescription.enabled = false;
        }
        
    }

    public void CloseDescription()
    {
        descriptionPanel.SetActive(false);
    }

    public void AddSlots()
    {
        RectTransform rectTransform = scrollPanel.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, rectTransform.offsetMin.y - 110);
        GameObject newSlot; 
        for (int i = 0; i < 6; i++)
        {
            newSlot = Instantiate(slotPrefab, scrollPanel.transform);
            equipmentSlots.Add(newSlot.GetComponent<EquipmentSlot>());
            
        }
    }
    

}
