using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{ 
    Consumable,
    EquipHead,
    EquipChest,
    EquipLegs,
    EquipBoots,
    EquipHand,
}

public class Item : MonoBehaviour
{

    [SerializeField]
    public ItemType itemType;

    [SerializeField]
    public int itemId;

    [SerializeField]
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    public string description;

    private void Start()
    {
        
    }

}
