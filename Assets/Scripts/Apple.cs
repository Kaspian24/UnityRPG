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
        ItemType = ItemType.Consumable;
        ItemName = "Apple";
        Description = "A sweet, juicy apple that restores some health.";
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
