using UnityEngine;

/// <summary>
/// Manages interaction message.
/// </summary>
public class InteractUIManager : MonoBehaviour
{
    public static InteractUIManager Instance { get; private set; }

    public GameObject interractInfoPanel;

    /// <summary>
    /// Initializes singleton.
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
    /// Toggles interact info panel.
    /// </summary>
    /// <param name="state">Interact info panel state. True to show, false to hide.</param>
    private void ToggleInterractInfo(bool state)
    {
        interractInfoPanel.SetActive(state);
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleInterractInfo += ToggleInterractInfo;
    }

    /// <summary>
    /// Unsubscribes to events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleInterractInfo -= ToggleInterractInfo;
    }
}
