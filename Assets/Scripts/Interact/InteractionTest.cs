using UnityEngine;

/// <summary>
/// Handles test interaction.
/// </summary>
public class InteractionTest : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Prints a message in debug log.
    /// </summary>
    public void Interact()
    {
        Debug.Log("Interakcja");
    }
}
