using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string id;

    // Start is called before the first frame update
    public void Interact()
    {
        GameObject.FindGameObjectWithTag("ChestController").GetComponent<ChestController>().OpenChest(id);
    }
}
