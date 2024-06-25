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
using static UnityEditor.Progress;
using static UnityEngine.EventSystems.EventTrigger;

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
    private int GOLD;


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
        LoadItems(SaveManager.Instance.currentSave.items);
        LoadEquippedItems(SaveManager.Instance.currentSave.equippedItems);
        loadStats(SaveManager.Instance.currentSave.stats);

        UpdateStats();
        UpdateMoney(GOLD);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().UpdateStats(HP, MaxHP, MP, MaxMP, STR, MAG, DEF);

        if (playerSlots[4].IsFull)
        {
            playerSlots[4].EquipItem();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(EXP == EXPTOLV)
        {
            LevelUp();
        }
    }

    public bool AddItem(Item item)
    {

        if (item.ItemType == ItemType.Consumable || item.ItemType == ItemType.KeyItem)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].IsFull && inventorySlots[i].Item.ItemId == item.ItemId)
                {
                    inventorySlots[i].AddExistingItem(item.Quantity);
                    return true;
                }
                if (!inventorySlots[i].IsFull)
                {

                    GameObject draggableItem = createNewDraggableItem(item, inventorySlots[i].transform);

                    inventorySlots[i].AddItem(draggableItem.GetComponent<DraggableItem>().Item);
                    draggableItem.transform.localScale = new Vector3(-draggableItem.transform.localScale.x,
                        -draggableItem.transform.localScale.y, -draggableItem.transform.localScale.z);
                    draggableItem.transform.localScale = new Vector3(2.78f, 2.78f, 2.78f);
                    return true;
                }

            }
            return false;
        }
        else
        {
            for (int i = 0; i < equipmentSlots.Count; i++)
            {
                if (!equipmentSlots[i].IsFull)
                {
                    GameObject draggableItem = createNewDraggableItem(item, equipmentSlots[i].transform);
                    equipmentSlots[i].AddItem(draggableItem.GetComponent<DraggableItem>().Item);
                    draggableItem.transform.localScale = new Vector3(-draggableItem.transform.localScale.x,
                        -draggableItem.transform.localScale.y, -draggableItem.transform.localScale.z);
                    draggableItem.transform.localScale = new Vector3(2.78f, 2.78f, 2.78f);
                    return true;
                }
            }
            return false;
        }
    }

    public GameObject createNewDraggableItem(Item item, Transform transform)
    {
        GameObject draggableItem = new GameObject("DraggableItem");
        draggableItem.AddComponent<Image>();
        draggableItem.AddComponent<DraggableItem>();
        draggableItem.GetComponent<DraggableItem>().Item = new Item();
        draggableItem.GetComponent<DraggableItem>().Item.copyItem(item);
        draggableItem.GetComponent<Image>().sprite = item.Sprite;
        draggableItem.GetComponent<DraggableItem>().parentAfterDrag = transform;
        draggableItem.GetComponent<DraggableItem>().Image = draggableItem.GetComponent<Image>();
        draggableItem.transform.SetParent(transform);
        return draggableItem;
    }

    public bool UseItem(Item item)
    {
        if(item.ItemType != ItemType.Consumable)
        {
            return false;
        }

        if(item.HP > 0 && HP < MaxHP)
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
        else if (item.MP > 0 && MP < MaxMP)
        {
            if ((MP + item.MP) <= MaxMP)
            {
                MP += item.MP;
            }
            else
            {
                MP = MaxMP;
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

        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().UpdateStats(HP, MaxHP, MP, MaxMP, STR, MAG, DEF);
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

        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().UpdateStats(HP, MaxHP, MP, MaxMP, STR, MAG, DEF);
    }

    public void UpdateHP(int hp)
    {
        HP = hp;

        StatsText.text = "HP - " + HP.ToString() + "/" + MaxHP.ToString()
            + "\n" + "MP - " + MP.ToString() + "/" + MaxMP.ToString()
            + "\n" + "STR - " + STR.ToString()
            + "\n" + "MAG - " + MAG.ToString()
            + "\n" + "DEF - " + DEF.ToString();
    }

    public void UpdateMP(int mp)
    {
        MP = mp;

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
        GOLD += amount;
        MoneyText.text = GOLD.ToString();
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

    //Metoda zapisuj¹ca przedmioty w ekwipunku
    public List<KeyValuePair<string, int>>SaveItems()
    {
        List<KeyValuePair<string, int>> items = new List<KeyValuePair<string, int>>();
        for(int i = 0; i < equipmentSlots.Count; i++)
        {
            if (equipmentSlots[i].IsFull)
            {
                items.Add(new KeyValuePair<string, int>(equipmentSlots[i].Item.ItemId, equipmentSlots[i].Item.Quantity));
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].IsFull)
            {
                items.Add(new KeyValuePair<string, int>(inventorySlots[i].Item.ItemId, inventorySlots[i].Item.Quantity));
            }
        }
        return items;
    }

    //Metoda zapisuj¹ca wyekwipowane przedmioty
    public List<KeyValuePair<string, string>>SaveEquippedItems()
    {
        List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
        for(int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i].IsFull)
            {
                items.Add(new KeyValuePair<string, string>(playerSlots[i].Item.ItemId, playerSlots[i].TypeList[0].ToString()));
            }

        }
        return items;
    }

    //Metoda wczytuj¹ca przedmioty do ekwipunku
    public void LoadItems(List<KeyValuePair<string, int>> items)
    {
        foreach(KeyValuePair<string, int> entry in items)
        {
            GameObject tempObject = Resources.Load("Prefabs/Items/" + entry.Key) as GameObject;
            Item tempItem = tempObject.GetComponent<SceneItem>().Item;
            Item i = new Item();
            i.copyItem(tempItem);
            i.Quantity = entry.Value;
            AddItem(i);
        }
    }

    //Metoda wczytuj¹ca wyekwipowane przedmioty
    public void LoadEquippedItems(List<KeyValuePair<string, string>> items)
    {
        foreach (KeyValuePair<string, string> entry in items)
        {
            GameObject tempObject = Resources.Load("Prefabs/Items/" + entry.Key) as GameObject;
            Item tempItem = tempObject.GetComponent<SceneItem>().Item;
            Item i = new Item();
            i.copyItem(tempItem);
            for(int j = 0; j < playerSlots.Length; j++)
            {
                if(entry.Value == playerSlots[j].TypeList[0].ToString())
                {
                    GameObject tempDrag = createNewDraggableItem(i, playerSlots[j].transform);
                    playerSlots[j].AddItem(tempDrag.GetComponent<DraggableItem>().Item);
                    tempDrag.transform.localScale = new Vector3(-tempDrag.transform.localScale.x,
                       -tempDrag.transform.localScale.y, -tempDrag.transform.localScale.z);
                    tempDrag.transform.localScale = new Vector3(2.78f, 2.78f, 2.78f);

                }
            }
        }

    }

    public Dictionary<string, int>saveStats()
    {
        Dictionary<string, int> stats = new Dictionary<string, int>();

        stats.Add("LEVEL", LEVEL);
        stats.Add("EXP", EXP);
        stats.Add("EXPTOLV", EXPTOLV);
        stats.Add("GOLD", GOLD);

        stats.Add("HP", HP);
        stats.Add("MaxHP", MaxHP);
        stats.Add("MP", MP);
        stats.Add("MaxMP", MaxMP);
        stats.Add("STR", STR);
        stats.Add("MAG", MAG);
        stats.Add("DEF", DEF);

        return stats;
    }

    public void loadStats(Dictionary<string, int> stats)
    {
        foreach(KeyValuePair<string, int> item in stats)
        {
            switch(item.Key)
            {
                case "LEVEL":
                    LEVEL = item.Value;
                    break;
                case "EXP":
                    EXP = item.Value;
                    break;
                case "EXPTOLV":
                    EXPTOLV = item.Value;
                    break;
                case "GOLD":
                    GOLD = item.Value;
                    break;
                case "HP":
                    HP = item.Value;
                    break;
                case "MaxHP":
                    MaxHP = item.Value;
                    break;
                case "MP":
                    MP = item.Value;
                    break;
                case "MaxMP":
                    MaxMP = item.Value;
                    break;
                case "STR":
                    STR = item.Value;
                    break;
                case "MAG":
                    MAG = item.Value;
                    break;
                case "DEF":
                    DEF = item.Value;
                    break;


            }
        }
    }
 
    


}
