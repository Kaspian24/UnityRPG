using UnityEngine;

public class GameFinishedManager : MonoBehaviour
{
    public static GameFinishedManager Instance { get; private set; }

    public GameObject gameFinishedPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void ToggleGameFinishedPanel(bool state)
    {
        gameFinishedPanel.SetActive(state);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleGameFinishedMessage += ToggleGameFinishedPanel;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleGameFinishedMessage -= ToggleGameFinishedPanel;
    }
}
