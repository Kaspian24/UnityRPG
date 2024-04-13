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
    private ItemType itemType;

    [SerializeField]
    public int itemId;

    [SerializeField]
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite sprite;

    [SerializeField]
    private string description;

    private void Start()
    {
        
    }

}
