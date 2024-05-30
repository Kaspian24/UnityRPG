using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{ 
    None,
    Consumable,
    EquipHead,
    EquipChest,
    EquipLegs,
    EquipBoots,
    EquipHand,
    KeyItem,
}

[System.Serializable]
public class Item : MonoBehaviour
{

    [Header("STATS")]
    [SerializeField]
    private int hp;
    [SerializeField]
    private int mp;
    [SerializeField]
    private int str;
    [SerializeField]
    private int mag;
    [SerializeField]
    private int def;

    [Header("DATA")]
    [SerializeField]
    private ItemType itemType;

    [SerializeField]
    private int itemId;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private string description;

    public ItemType ItemType { get => itemType; }
    public int ItemId { get => itemId; }
    public string ItemName { get => itemName; }
    public int Quantity { get => quantity; set => quantity = value; }
    public Sprite Sprite { get => sprite; }
    public string Description { get => description; }
    public int HP { get => hp; }
    public int MP { get => mp; }
    public int STR { get => str; }
    public int MAG { get => mag; }
    public int DEF { get => def; }

    public void copyItem(Item item)
    {
        this.hp = item.HP;
        this.mp = item.MP;
        this.str = item.STR;
        this.mag = item.MAG;
        this.def = item.DEF;
        this.itemType = item.ItemType;
        this.itemId = item.itemId;
        this.itemName = String.Copy(item.ItemName);
        this.quantity = item.Quantity;
        this.sprite = item.Sprite;
        this.description = String.Copy(item.Description);
    }

    public string PrintStats()
    {
        string returnedStats = "";
        if(hp != 0)
        {
            returnedStats += "\nMax HP: " + hp;
        }
        if(mp != 0)
        {
            returnedStats += "\nMax MP: " + mp;
        }
        if(str != 0)
        {
            returnedStats += "\nSTR: " + str;
        }
        if(mag != 0)
        {
            returnedStats += "\nMAG: " + mag;
        }
        if(def != 0)
        {
            returnedStats += "\nDEF: " + def;
        }

        return returnedStats;
    }
}

