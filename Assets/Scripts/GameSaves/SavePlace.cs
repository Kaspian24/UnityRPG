using UnityEngine;

/// <summary>
/// Handles save place interaction.
/// </summary>
public class SavePlace : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Opens save menu on interaction.
    /// </summary>
    public void Interact()
    {
        GameEventsManager.Instance.gameModeEvents.SaveMenuOnOff(true);
    }
}
