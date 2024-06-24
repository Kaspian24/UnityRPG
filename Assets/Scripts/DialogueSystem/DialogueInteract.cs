using UnityEngine;

public class DialogueInteract : MonoBehaviour, IInteractable
{
    [Header("Ink Json Dialogue")]
    [SerializeField]
    private TextAsset inkJson;
    public bool randomDialogue = false;
    public void Interact()
    {
        if (randomDialogue)
        {
            DialogueManager.Instance.StartRandomDialogue();
        }
        else
        {
            DialogueManager.Instance.StartDialogue(inkJson);
        }
    }
}
