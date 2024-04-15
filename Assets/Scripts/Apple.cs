using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Apple : Item
{
    [SerializeField]
    private int healthRegain;

    public Apple()
    {
        itemType = ItemType.Consumable;
        itemName = "Apple";
        description = "A sweet, juicy apple that restores some health.";
        healthRegain = 10;
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
