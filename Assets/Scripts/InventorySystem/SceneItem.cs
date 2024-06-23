using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItem : MonoBehaviour, IInteractable
{

    [SerializeField]
    private Item item;

    public Item Item { get => item; set => item = value; }

    public void Interact()
    {
        GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<InventoryManager>().AddItem(Item);
        Destroy(this.gameObject);
    }

}
