using UnityEngine;

public class LoadMenuManager : MonoBehaviour
{
    public static LoadMenuManager Instance { get; private set; }

    public GameObject loadMenuPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void ToggleLoadMenu(bool state)
    {
        loadMenuPanel.SetActive(state);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleLoadMenu += ToggleLoadMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleLoadMenu -= ToggleLoadMenu;
    }
}
