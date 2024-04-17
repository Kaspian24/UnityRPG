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
}

[System.Serializable]
public struct Stats
{
    public int HP;
    public int MP;
    public int STR;
    public int MAG;
    public int DEF;
}

public class Item : MonoBehaviour
{
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

    [SerializeField]
    private Stats stats;

    public ItemType ItemType { get => itemType; set => itemType = value; }
    public int ItemId { get => itemId; set => itemId = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public string Description { get => description; set => description = value; }
    public Stats Stats { get => stats; set => stats = value; }

    private void Start()
    {
        
    }

    public void swapStats(Item item)
    {
        (this.Stats, item.Stats) = (item.Stats, this.Stats);
    }


}
