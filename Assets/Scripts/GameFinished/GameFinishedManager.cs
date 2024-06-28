using UnityEngine;

/// <summary>
/// Manages game finished behavoiur.
/// </summary>
public class GameFinishedManager : MonoBehaviour
{
    public static GameFinishedManager Instance { get; private set; }

    public GameObject gameFinishedPanel;

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
    /// Toggles game finished panel.
    /// </summary>
    /// <param name="state">Game finished panel state. True to show, false to hide.</param>
    private void ToggleGameFinishedPanel(bool state)
    {
        gameFinishedPanel.SetActive(state);
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleGameFinishedMessage += ToggleGameFinishedPanel;
    }

    /// <summary>
    /// Unsubscribes to events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleGameFinishedMessage -= ToggleGameFinishedPanel;
    }
}
