using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that manages items dropped on scene
/// </summary>
public class SceneItem : MonoBehaviour, IInteractable
{

    [SerializeField]
    private Item item;

    public Item Item { get => item; set => item = value; }

    /// <summary>
    /// Adds an item to the inventory if interacted with
    /// </summary>
    public void Interact()
    {
        if (GameObject.FindGameObjectWithTag("InventoryCanvas").GetComponent<InventoryManager>().AddItem(Item))
        {
            Destroy(this.gameObject);
        }
    }

}
