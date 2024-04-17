using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Item
{
    [SerializeField]
    private int armor;

    public Helmet()
    {
        ItemType = ItemType.EquipHead;
        ItemName = "Helmet";
        Description = "Helmet that give you some protection.";
        armor = 15;
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
