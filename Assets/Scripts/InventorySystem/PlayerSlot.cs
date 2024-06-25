using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerSlot : EquipmentSlot
{
    //public GameObject weapon;
    public Transform weaponPoint;

    private void Update()
    {
        if(transform.childCount == 0 && IsFull)
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
            if(draggableItem.Item.ItemType == ItemType.EquipHand)
            {
                EquipItem();
            }
        }
            
    }

    public override void DropItem()
    {
        InventoryManager.UpdateStats(Item, false);
        UnequipItem();
        base.DropItem();

    }

    public void EquipItem()
    {
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
        if(weaponSlot.tag == "Spellbook")
        {
            weaponSlot.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void UnequipItem()
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("PlayerHand");
        if (weapon.transform.childCount != 0)
        {
            Destroy(weapon.transform.GetChild(0).gameObject);
        }
    }
}
