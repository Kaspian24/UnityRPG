using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class that manages slots with equipped items
/// </summary>
public class PlayerSlot : EquipmentSlot
{
    //public GameObject weapon;
    public Transform weaponPoint;


    /// <summary>
    /// Deletes or adds and object if dragged to or away but not added to another slot
    /// </summary>
    private void Update()
    {
        if (transform.childCount == 0 && IsFull)
        {
            InventoryManager.UpdateStats(Item, false);
            UnequipItem();
            DeleteItem();
        }
        if (transform.childCount == 1 && !IsFull)
        {
            Transform DraggedItem = this.transform.GetChild(0);
            AddItem(DraggedItem.GetComponent<DraggableItem>().Item);
            InventoryManager.UpdateStats(Item, true);
            EquipItem();
        }
    }

    /// <inheritdoc/>
    public override void OnDrop(PointerEventData eventData)
    {

        if (IsFull)
        {
            return;
        }
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem;
        if ((draggableItem = dropped.GetComponent<DraggableItem>()) && (CheckTypeList(draggableItem.Item)))
        {
            draggableItem.parentAfterDrag = transform;
            AddItem(draggableItem.Item);
            InventoryManager.UpdateStats(draggableItem.Item, true);
            if (draggableItem.Item.ItemType == ItemType.EquipHand)
            {
                EquipItem();
            }
        }

    }

    /// <summary>
    /// Drops item on the ground and unequips it
    /// </summary>
    public override void DropItem()
    {
        InventoryManager.UpdateStats(Item, false);
        UnequipItem();
        base.DropItem();

    }

    /// <summary>
    /// Creates a weapon object and puts it in player's hand
    /// </summary>
    public void EquipItem()
    {
        if (TypeList[0] != ItemType.EquipHand)
            return;
        Transform weaponPoint = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<Transform>();

        // Utwórz instancjê prefabrykatu
        GameObject weaponSlot = Instantiate(ItemPrefab);

        weaponSlot.GetComponent<SceneItem>().enabled = false;

        // Ustaw rodzica dla nowo utworzonej instancji
        weaponSlot.transform.SetParent(weaponPoint);

        // Opcjonalnie zresetuj lokaln¹ pozycjê, rotacjê i skalowanie nowego obiektu
        weaponSlot.transform.localPosition = Vector3.zero;
        weaponSlot.transform.localRotation = Quaternion.Euler(Vector3.zero);
        weaponSlot.transform.localScale = Vector3.one;
        weaponSlot.GetComponent<Rigidbody>().isKinematic = true;
        weaponSlot.GetComponent<Rigidbody>().detectCollisions = false;
        weaponSlot.GetComponent<Collider>().enabled = false;
        if (weaponSlot.tag == "Spellbook")
        {
            weaponSlot.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// Deletes weapon object from player's hand
    /// </summary>
    public void UnequipItem()
    {
        if (TypeList[0] != ItemType.EquipHand)
            return;

        GameObject weapon = GameObject.FindGameObjectWithTag("PlayerHand");
        if (weapon.transform.childCount != 0 && Item.ItemType == ItemType.EquipHand)
        {
            Destroy(weapon.transform.GetChild(0).gameObject);
        }
    }
}
