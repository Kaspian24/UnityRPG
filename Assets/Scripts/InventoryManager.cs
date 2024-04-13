using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject InventoryMenu;

    [SerializeField]
    private Camera uiCamera;

    [SerializeField]
    private InventorySlot[] itemList;

    [SerializeField]
    private TextMeshProUGUI description;

    private bool menuActivated;
    void Start()
    {
        GameObject item = GameObject.FindGameObjectWithTag("Item");
        Item i = item.GetComponent<Item>();
        AddItem(i.itemId, i.itemName, i.quantity, i.sprite);
        Destroy(item);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && menuActivated)
        {
            InventoryMenu.SetActive(false);
            uiCamera.enabled = false;
            menuActivated = false;
        }
        else if(Input.GetKeyDown(KeyCode.I) && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            uiCamera.enabled = true;
            menuActivated = true;
        }

    }

    public void AddItem(int itemId, string itemName, int quantity, Sprite sprite)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (!itemList[i].isFull)
            {
                itemList[i].AddItem(itemId, itemName, quantity, sprite);
                break;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            itemList[i].selectedShader.SetActive(false);
            itemList[i].isSelected = false;
        }
    }

}
