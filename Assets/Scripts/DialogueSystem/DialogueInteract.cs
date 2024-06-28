using UnityEngine;

/// <summary>
/// Handles dialogue start on interaction.
/// </summary>
public class DialogueInteract : MonoBehaviour, IInteractable
{
    [Header("Ink Json Dialogue")]
    [SerializeField]
    private TextAsset inkJson;
    public bool randomDialogue = false;
    /// <summary>
    /// Launches selected dialogue or random dialogue on interaction.
    /// </summary>
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
