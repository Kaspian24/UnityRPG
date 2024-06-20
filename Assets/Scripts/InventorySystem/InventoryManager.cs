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
    private int LEVEL;
    [SerializeField]
    private int EXP;
    [SerializeField]
    private int EXPTOLV;

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

    [SerializeField]
    private int money;


    [Header("UI ELEMENTS")]
    [SerializeField]
    private TextMeshProUGUI LevelText;
    [SerializeField]
    private TextMeshProUGUI ExpText;

    [SerializeField]
    private TextMeshProUGUI StatsText;

    [SerializeField]
    private TextMeshProUGUI MoneyText;

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
    [SerializeField]
    private GameObject moneyPanel;
    [SerializeField]
    private GameObject levelUpPanel;
    [SerializeField]
    private TextMeshProUGUI levelUpText;


    void Start()
    {
        UpdateStats();
        UpdateMoney(money);

        //GameObject item;
        //if (item = GameObject.FindGameObjectWithTag("Item"))
        //{
        //    Item i = item.GetComponent<DraggableItem>().Item;
        //    AddItem(i);
        //    Destroy(item);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(EXP == EXPTOLV)
        {
            LevelUp();
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
        LevelText.text = "LEVEL - " + LEVEL.ToString();
        ExpText.text = "EXP - " + EXP.ToString() + "/" + EXPTOLV.ToString();

        StatsText.text = "HP - " + HP.ToString() + "/" + MaxHP.ToString()
            + "\n" + "MP - " + MP.ToString() + "/" + MaxMP.ToString()
            + "\n" + "STR - " + STR.ToString()
            + "\n" + "MAG - " + MAG.ToString()
            + "\n" + "DEF - " + DEF.ToString();
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

        StatsText.text = "HP - " + HP.ToString() + "/" + MaxHP.ToString()
            + "\n" + "MP - " + MP.ToString() + "/" + MaxMP.ToString()
            + "\n" + "STR - " + STR.ToString()
            + "\n" + "MAG - " + MAG.ToString()
            + "\n" + "DEF - " + DEF.ToString();


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

    public void CloseLevelUp()
    {
        levelUpPanel.SetActive(false);
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

    public void LevelUp()
    {

        float baseEXP = 100;
        float exponent = 1.5f;
        System.Random random = new System.Random();
        int tmp;
        levelUpText.text = "";

        if(EXP == EXPTOLV)
        {
            LEVEL += 1;

            levelUpText.text += "Level: " + LEVEL.ToString() + "\n";

            tmp = random.Next(10, 21);
            MaxHP += tmp;
            levelUpText.text += "Max HP +" + tmp.ToString() + "\n"; ;

            tmp = random.Next(10, 21);
            MaxMP += tmp;
            levelUpText.text += "Max MP +" + tmp.ToString() + "\n"; ;

            tmp = random.Next(1, 5);
            STR += tmp;
            levelUpText.text += "STR +" + tmp.ToString() + "\n"; ;

            tmp = random.Next(1, 5);
            MAG += tmp;
            levelUpText.text += "MAG +" + tmp.ToString() + "\n"; ;

            tmp = random.Next(1, 5);
            DEF += tmp;
            levelUpText.text += "DEF +" + tmp.ToString() + "\n"; ;

            EXP = 0;
            EXPTOLV = (int)Math.Floor(baseEXP * (Math.Pow(LEVEL, exponent)));

            levelUpPanel.SetActive(true);


            UpdateStats();
        }
    }

    private void UpdateMoney(int amount)
    {
        money += amount;
        MoneyText.text = money.ToString();
    }

    private void ToggleInventoryMenu(bool state)
    {
            InventoryPanel.SetActive(state);
            InventoryMenu.SetActive(state);
            moneyPanel.SetActive(state);
            levelUpPanel.SetActive(false);
            descriptionPanel.SetActive(false);
            EquipmentMenu.SetActive(false);
            
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleInventory += ToggleInventoryMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleInventory -= ToggleInventoryMenu;
    }

    


}
