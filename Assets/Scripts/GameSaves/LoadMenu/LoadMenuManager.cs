using UnityEngine;

/// <summary>
/// Manages load menu.
/// </summary>
public class LoadMenuManager : MonoBehaviour
{
    public static LoadMenuManager Instance { get; private set; }

    public GameObject loadMenuPanel;

    /// <summary>
    /// Singleton instance
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
    /// Toggles load menu panel.
    /// </summary>
    /// <param name="state">Panel state. True to show, false to hide.</param>
    private void ToggleLoadMenu(bool state)
    {
        loadMenuPanel.SetActive(state);
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleLoadMenu += ToggleLoadMenu;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleLoadMenu -= ToggleLoadMenu;
    }
}
