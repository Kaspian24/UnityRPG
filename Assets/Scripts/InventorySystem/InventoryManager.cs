using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject InventoryMenu;

    [SerializeField]
    private GameObject EquipmentMenu;

    [SerializeField]
    private Camera uiCamera;

    [SerializeField]
    private InventorySlot[] inventorySlots;

    [SerializeField]
    private EquipmentSlot[] equipmentSlots;

    [SerializeField]
    private PlayerSlot[] playerSlots;

    [SerializeField]
    private TextMeshProUGUI description;

    private bool inventoryActivated;
    private bool equipmentActivated;


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


    void Start()
    {
        GameObject item = GameObject.FindGameObjectWithTag("Item");
        Item i = item.GetComponent<Item>();
        AddItem(i);
        AddItem(i);
        Destroy(item);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && (inventoryActivated || equipmentActivated))
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            uiCamera.enabled = false;
            inventoryActivated = false;
            equipmentActivated = false;
        }
        else if(Input.GetKeyDown(KeyCode.I) && !inventoryActivated && !equipmentActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            uiCamera.enabled = true;
            inventoryActivated = true;
        }
        else if(Input.GetKeyDown(KeyCode.E) && inventoryActivated)
        {
            InventoryMenu.SetActive(false);
            inventoryActivated = false;
            EquipmentMenu.SetActive(true);
            equipmentActivated = true;

        }
        else if(Input.GetKeyDown(KeyCode.Q) && equipmentActivated)
        {
            InventoryMenu.SetActive(true);
            inventoryActivated=true;
            EquipmentMenu.SetActive(false);
            equipmentActivated = false;
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
                    inventorySlots[i].AddItem(item);
                    break;
                }
                if (inventorySlots[i].ItemId == item.ItemId)
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
                    equipmentSlots[i].AddItem(item);
                    break;
                }
            }
        }
    }

    public bool EquipItem(Item item)
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i].ItemType == item.ItemType && !playerSlots[i].IsFull)
            {
                playerSlots[i].EquipItem(item);
                UpdateStats(playerSlots[i], true);
                return true;
            }
        }
        return false;
    }

    public bool UseItem(Item item)
    {
        if (HP < MaxHP)
        {
            if ((HP + item.Stats.HP) <= MaxHP)
            {
                HP += item.Stats.HP;
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
            MaxHP += item.Stats.HP;
            MaxMP += item.Stats.MP;
            STR += item.Stats.STR;
            MAG += item.Stats.MAG;
            DEF += item.Stats.DEF;
        }
        else if(!equipped)
        {
            MaxHP -= item.Stats.HP;
            MaxMP -= item.Stats.MP;
            STR -= item.Stats.STR;
            MAG -= item.Stats.MAG;
            DEF -= item.Stats.DEF;
        }

        HpText.text = "HP - " + HP.ToString() + "/" + MaxHP.ToString();
        MpText.text = "MP - " + MP.ToString() + "/" + MaxMP.ToString();
        StrText.text = "STR - " + STR.ToString();
        MagText.text = "MAG - " + MAG.ToString();
        DefText.text = "DEF - " + DEF.ToString();


    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SelectedShader.SetActive(false);
            inventorySlots[i].IsSelected = false;
        }
        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].SelectedShader.SetActive(false);
            equipmentSlots[i].IsSelected = false;
        }
        for (int i = 0; i < playerSlots.Length; i++)
        {
            playerSlots[i].SelectedShader.SetActive(false);
            playerSlots[i].IsSelected = false;
        }
    }

}
