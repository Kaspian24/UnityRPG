using UnityEngine;

[System.Serializable]
public class QuestGiver : MonoBehaviour
{
    [Header("Ink Json Dialogue")]
    [SerializeField]
    private TextAsset inkJson;

    public bool randomNPC = false;

    private void Awake()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Renderer>().material.color = Color.green;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }
        if (randomNPC)
        {
            DialogueManager.Instance.StartRandomDialogue();
            return;
        }
        DialogueManager.Instance.StartDialogue(inkJson);
    }
}
