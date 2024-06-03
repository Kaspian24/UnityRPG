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
[Serializable]
public class Item
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

    public ItemType ItemType { get => itemType; set => itemType = value; }
    public int ItemId { get => itemId; set => itemId = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public string Description { get => description; set => description = value; }
    public int HP { get => hp; set => hp = value; }
    public int MP { get => mp; set => mp = value; }
    public int STR { get => str; set => str = value; }
    public int MAG { get => mag; set => mag = value; }
    public int DEF { get => def; set => def = value; }

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
            returnedStats += "\nHP: " + hp;
        }
        if(mp != 0)
        {
            returnedStats += "\nMP: " + mp;
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

