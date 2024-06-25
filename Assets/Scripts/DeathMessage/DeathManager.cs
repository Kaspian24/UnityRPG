using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance { get; private set; }

    public GameObject deathMessagePanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void ToggleDeathMessage(bool state)
    {
        deathMessagePanel.SetActive(state);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleDeathMessage += ToggleDeathMessage;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleDeathMessage -= ToggleDeathMessage;
    }
}
