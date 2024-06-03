using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ChestController : MonoBehaviour
{
    // Start is called before the first frame update

    [Serializable]
    private class ItemData
    {
        public int hp;
        public int mp;
        public int str;
        public int mag;
        public int def;

        public ItemType itemType;
        public int itemId;
        public string itemName;
        public int quantity;
        public string spriteId;
        public string description;
    }

    [Serializable]
    private class ItemList
    {
        public List<ItemData> items = new List<ItemData>();
    }

    [SerializeField]
    private EquipmentSlot[] equipmentSlots;

    void Start()
    {
        //TODO String z nazw¹ pliku bêdzie przekazywany z obiektu na scenie
        FromJson("0");
    }

    public void ToJson()
    {
        ItemList itemList = new ItemList();

        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].IsFull)
            {
                itemList.items.Add(ItemToItemData(equipmentSlots[i].Item));
            }
        }

        string json = JsonUtility.ToJson(itemList);

        File.WriteAllText("Assets/Resources/Prefabs/InventorySystem/ChestJsons/0.json", json);
        AssetDatabase.ImportAsset("Assets/Resources/Prefabs/InventorySystem/ChestJsons/0.json");
    }

    public void FromJson(string id)
    {
        String json = File.ReadAllText("Assets/Resources/Prefabs/InventorySystem/ChestJsons/0.json");
        ItemList itemList = JsonUtility.FromJson<ItemList>(json);
        for (int i = 0; i < itemList.items.Count; i++)
        {
            GameObject draggableItem = new GameObject("DraggableItem");
            draggableItem.AddComponent<Image>();
            draggableItem.AddComponent<DraggableItem>();
            draggableItem.GetComponent<DraggableItem>().Item = ItemDataToItem(itemList.items[i]);
            Item newItem = draggableItem.GetComponent<DraggableItem>().Item;
            draggableItem.GetComponent<Image>().sprite = newItem.Sprite;
            draggableItem.GetComponent<DraggableItem>().parentAfterDrag = equipmentSlots[i].transform;
            draggableItem.GetComponent<DraggableItem>().Image = draggableItem.GetComponent<Image>();

            draggableItem.transform.SetParent(equipmentSlots[i].transform);

            equipmentSlots[i].AddItem(newItem);

        }

    }

    private ItemData ItemToItemData(Item item)
    {
        ItemData newData = new ItemData();

        newData.hp = item.HP;
        newData.mp = item.MP;
        newData.str = item.STR;
        newData.mag = item.MAG;
        newData.def = item.DEF;
        newData.itemType = item.ItemType;
        newData.itemName = String.Copy(item.ItemName);
        newData.itemId = item.ItemId;
        newData.quantity = item.Quantity;
        newData.description = String.Copy(item.Description);
        newData.spriteId = String.Copy(AssetDatabase.GetAssetPath(item.Sprite));


        return newData;
    }

    private Item ItemDataToItem(ItemData itemData)
    {
        Item newItem = new Item();

        newItem.HP = itemData.hp;
        newItem.MP = itemData.mp;
        newItem.STR = itemData.str;
        newItem.MAG = itemData.mag;
        newItem.DEF = itemData.def;
        newItem.ItemName = String.Copy(itemData.itemName);
        newItem.ItemType = itemData.itemType;
        newItem.ItemId = itemData.itemId;
        newItem.Quantity = itemData.quantity;
        newItem.Description = String.Copy(itemData.description);
        newItem.Sprite = AssetDatabase.LoadAssetAtPath<Sprite>(itemData.spriteId);
        return newItem;
    }

    public void TestJson()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        ToJson();
    }

}
