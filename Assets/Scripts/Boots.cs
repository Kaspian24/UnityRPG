using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boots : Item
{
    [SerializeField]
    private int armor;

    public Boots()
    {
        itemType = ItemType.EquipBoots;
        itemName = "Boots";
        description = "Boots that give you some protection.";
        armor = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
