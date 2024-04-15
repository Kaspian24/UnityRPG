using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pants : Item
{
    [SerializeField]
    private int armor;

    public Pants()
    {
        itemType = ItemType.EquipLegs;
        itemName = "Pants";
        description = "Pants that give you some protection.";
        armor = 20;
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
