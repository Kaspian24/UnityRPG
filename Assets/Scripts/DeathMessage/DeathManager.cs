using UnityEngine;

/// <summary>
/// Manages death messages.
/// </summary>
public class DeathManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static DeathManager Instance { get; private set; }

    /// <summary>
    /// Death message panel.
    /// </summary>
    public GameObject deathMessagePanel;

    /// <summary>
    /// Singleton initialization.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Toggles death message panel.
    /// </summary>
    /// <param name="state">Death message panel state. True to show, false to hide.</param>
    private void ToggleDeathMessage(bool state)
    {
        deathMessagePanel.SetActive(state);
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleDeathMessage += ToggleDeathMessage;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleDeathMessage -= ToggleDeathMessage;
    }
}
