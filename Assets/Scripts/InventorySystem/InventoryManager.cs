using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that manages the entire inventory system
/// </summary>
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

    /// <summary>
    /// Loads data from current save and initializes objects
    /// </summary>
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

    /// <summary>
    /// Levels up player if they collect enough experience
    /// </summary>
    void Update()
    {
        if (EXP >= EXPTOLV)
        {
            LevelUp();
        }
    }


    /// <summary>
    /// Adds the item to inventory, based on it's type. Creates a draggable item object and puts it in first free slot in the correct inventory tab
    /// </summary>
    /// <param name="item"></param>
    /// <returns>True if item was succesfully added. False if all slots are occupied</returns>
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

    /// <summary>
    /// Creates new draggable item object
    /// </summary>
    /// <param name="item"></param>
    /// <param name="transform">Parent transform</param>
    /// <returns>GameObject with DraggableItem component</returns>
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

    /// <summary>
    /// Use given item and update stats
    /// </summary>
    /// <param name="item"></param>
    /// <returns>True if item was used. False otherwise.</returns>
    public bool UseItem(Item item)
    {
        if (item.ItemType != ItemType.Consumable)
        {
            return false;
        }

        if (item.HP > 0 && HP < MaxHP)
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

    /// <summary>
    /// Updates stats in UI
    /// </summary>
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

    /// <summary>
    /// Updates stats with given item parameters. Adds if equipped. Subtracts if not
    /// </summary>
    /// <param name="item"></param>
    /// <param name="equipped"></param>
    public void UpdateStats(Item item, bool equipped)
    {
        if (equipped)
        {
            MaxHP += item.HP;
            MaxMP += item.MP;
            STR += item.STR;
            MAG += item.MAG;
            DEF += item.DEF;
        }
        else if (!equipped)
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

    /// <summary>
    /// Updates hp stat
    /// </summary>
    /// <param name="hp"></param>
    public void UpdateHP(int hp)
    {
        HP = hp;

        StatsText.text = "HP - " + HP.ToString() + "/" + MaxHP.ToString()
            + "\n" + "MP - " + MP.ToString() + "/" + MaxMP.ToString()
            + "\n" + "STR - " + STR.ToString()
            + "\n" + "MAG - " + MAG.ToString()
            + "\n" + "DEF - " + DEF.ToString();
    }

    /// <summary>
    /// Updates mp stat
    /// </summary>
    /// <param name="mp"></param>
    public void UpdateMP(int mp)
    {
        MP = mp;

        StatsText.text = "HP - " + HP.ToString() + "/" + MaxHP.ToString()
            + "\n" + "MP - " + MP.ToString() + "/" + MaxMP.ToString()
            + "\n" + "STR - " + STR.ToString()
            + "\n" + "MAG - " + MAG.ToString()
            + "\n" + "DEF - " + DEF.ToString();
    }

    /// <summary>
    /// Opens inventory tab
    /// </summary>
    public void openInventory()
    {
        InventoryMenu.SetActive(true);
        EquipmentMenu.SetActive(false);
        CloseDescription();
    }

    /// <summary>
    /// Opens equipment tab
    /// </summary>
    public void openEquipment()
    {
        InventoryMenu.SetActive(false);
        EquipmentMenu.SetActive(true);
        CloseDescription();
    }

    /// <summary>
    /// Opens description window with description of given item
    /// </summary>
    /// <param name="item"></param>
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

                panelRect.offsetMin = new Vector2(panelRect.offsetMin.x, panelRect.offsetMin.y - descriptionRect.offsetMin.y - 100);
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

    /// <summary>
    /// Closes desciption window
    /// </summary>
    public void CloseDescription()
    {
        descriptionPanel.SetActive(false);
    }

    /// <summary>
    /// Closes level up window
    /// </summary>
    public void CloseLevelUp()
    {
        GameEventsManager.Instance.gameModeEvents.LevelUpMessageOnOff(false);
    }

    /// <summary>
    /// Toggles level up window's state
    /// </summary>
    /// <param name="state"></param>
    public void ToggleLevelUp(bool state)
    {
        levelUpPanel.SetActive(state);
    }

    /// <summary>
    /// Adds new equipment slots in equipment tab (unused)
    /// </summary>
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

    /// <summary>
    /// Adds given amount to EXP variable and updates UI
    /// </summary>
    /// <param name="amount"></param>
    public void AddExp(int amount)
    {
        EXP += amount;
        ExpText.text = "EXP - " + EXP.ToString() + "/" + EXPTOLV.ToString();
    }

    /// <summary>
    /// Levels up player's stats
    /// </summary>
    public void LevelUp()
    {

        float baseEXP = 100;
        float exponent = 1.5f;
        System.Random random = new System.Random();
        int tmp;
        levelUpText.text = "";

        if (EXP >= EXPTOLV)
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



            EXP = EXP - EXPTOLV;
            EXPTOLV = (int)Math.Floor(baseEXP * (Math.Pow(LEVEL, exponent)));

            GameEventsManager.Instance.gameModeEvents.LevelUpMessageOnOff(true);

            UpdateStats();
        }
    }

    /// <summary>
    /// Adds given amount to GOLD stat an updates UI
    /// </summary>
    /// <param name="amount"></param>
    private void UpdateMoney(int amount)
    {
        GOLD += amount;
        MoneyText.text = GOLD.ToString();
    }

    /// <summary>
    /// Toggles inventory menu with given state
    /// </summary>
    /// <param name="state"></param>
    private void ToggleInventoryMenu(bool state)
    {
        InventoryPanel.SetActive(state);
        InventoryMenu.SetActive(state);
        moneyPanel.SetActive(state);
        descriptionPanel.SetActive(false);
        EquipmentMenu.SetActive(false);

    }

    /// <summary>
    /// Adds given item to inventory 
    /// </summary>
    /// <param name="item"></param>
    private void ItemAddEvenHandler(Item item)
    {
        _ = AddItem(item);
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleInventory += ToggleInventoryMenu;
        GameEventsManager.Instance.gameModeEvents.OnToggleLevelUpMessage += ToggleLevelUp;

        GameEventsManager.Instance.playerEvents.OnGoldAdd += UpdateMoney;
        GameEventsManager.Instance.playerEvents.OnExpAdd += AddExp;
        GameEventsManager.Instance.playerEvents.OnItemAdd += ItemAddEvenHandler;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleInventory -= ToggleInventoryMenu;
        GameEventsManager.Instance.gameModeEvents.OnToggleLevelUpMessage -= ToggleLevelUp;

        GameEventsManager.Instance.playerEvents.OnGoldAdd -= UpdateMoney;
        GameEventsManager.Instance.playerEvents.OnExpAdd -= AddExp;
        GameEventsManager.Instance.playerEvents.OnItemAdd -= ItemAddEvenHandler;
    }

    /// <summary>
    /// Creates a list of items in inventory and equipment to a list
    /// </summary>
    /// <returns>List of pairs (itemId, Quantity)</returns>
    public List<KeyValuePair<string, int>> SaveItems()
    {
        List<KeyValuePair<string, int>> items = new List<KeyValuePair<string, int>>();
        for (int i = 0; i < equipmentSlots.Count; i++)
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

    /// <summary>
    /// Creates a list of equipped items
    /// </summary>
    /// <returns>List of pairs (itemId, itemType)</returns>
    public List<KeyValuePair<string, string>> SaveEquippedItems()
    {
        List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i].IsFull)
            {
                items.Add(new KeyValuePair<string, string>(playerSlots[i].Item.ItemId, playerSlots[i].TypeList[0].ToString()));
            }

        }
        return items;
    }

    /// <summary>
    /// Adds items to inventory based on given list of pairs (itemId, quantity)
    /// </summary>
    /// <param name="items"></param>
    public void LoadItems(List<KeyValuePair<string, int>> items)
    {
        foreach (KeyValuePair<string, int> entry in items)
        {
            GameObject tempObject = Resources.Load("Prefabs/Items/" + entry.Key) as GameObject;
            Item tempItem = tempObject.GetComponent<SceneItem>().Item;
            Item i = new Item();
            i.copyItem(tempItem);
            i.Quantity = entry.Value;
            AddItem(i);
        }
    }

    /// <summary>
    /// Equips items based on list of pairs (itemId, itemType)
    /// </summary>
    /// <param name="items"></param>
    public void LoadEquippedItems(List<KeyValuePair<string, string>> items)
    {
        foreach (KeyValuePair<string, string> entry in items)
        {
            GameObject tempObject = Resources.Load("Prefabs/Items/" + entry.Key) as GameObject;
            Item tempItem = tempObject.GetComponent<SceneItem>().Item;
            Item i = new Item();
            i.copyItem(tempItem);
            for (int j = 0; j < playerSlots.Length; j++)
            {
                if (entry.Value == playerSlots[j].TypeList[0].ToString())
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

    /// <summary>
    /// Creates a dictionary containing player stats
    /// </summary>
    /// <returns>Dictionary containing player stats</returns>
    public Dictionary<string, int> saveStats()
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

    /// <summary>
    /// Changes player stats based on a given dictionary object
    /// </summary>
    /// <param name="stats"></param>
    public void loadStats(Dictionary<string, int> stats)
    {
        foreach (KeyValuePair<string, int> item in stats)
        {
            switch (item.Key)
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
