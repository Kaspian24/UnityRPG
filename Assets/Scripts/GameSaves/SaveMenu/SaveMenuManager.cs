using UnityEngine;

public class SaveMenuManager : MonoBehaviour
{
    public static SaveMenuManager Instance { get; private set; }

    public GameObject saveMenuPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void ToggleSaveMenu(bool state)
    {
        saveMenuPanel.SetActive(state);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleSaveMenu += ToggleSaveMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleSaveMenu -= ToggleSaveMenu;
    }
}
