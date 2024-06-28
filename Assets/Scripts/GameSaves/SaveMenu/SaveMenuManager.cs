using UnityEngine;

/// <summary>
/// Manages save menu.
/// </summary>
public class SaveMenuManager : MonoBehaviour
{
    public static SaveMenuManager Instance { get; private set; }

    public GameObject saveMenuPanel;

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
    /// Toggles save menu panel.
    /// </summary>
    /// <param name="state">Panel state. True to show, false to hide.</param>
    private void ToggleSaveMenu(bool state)
    {
        saveMenuPanel.SetActive(state);
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleSaveMenu += ToggleSaveMenu;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleSaveMenu -= ToggleSaveMenu;
    }
}
