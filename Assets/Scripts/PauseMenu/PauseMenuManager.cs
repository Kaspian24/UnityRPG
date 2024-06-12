using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance { get; private set; }

    public GameObject pauseMenuPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void TogglePauseMenu(bool state)
    {
        pauseMenuPanel.SetActive(state);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnTogglePauseMenu += TogglePauseMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnTogglePauseMenu -= TogglePauseMenu;
    }
}
