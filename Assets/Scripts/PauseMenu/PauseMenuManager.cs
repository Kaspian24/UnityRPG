using UnityEngine;

/// <summary>
/// Manages pause menu.
/// </summary>
public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance { get; private set; }

    public GameObject pauseMenuPanel;

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
    /// Toggles pause menu panel.
    /// </summary>
    /// <param name="state">Panel state. True to show, false to hide.</param>
    private void TogglePauseMenu(bool state)
    {
        pauseMenuPanel.SetActive(state);
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnTogglePauseMenu += TogglePauseMenu;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnTogglePauseMenu -= TogglePauseMenu;
    }
}
