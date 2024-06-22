using UnityEngine;

public class InteractUIManager : MonoBehaviour
{
    public static InteractUIManager Instance { get; private set; }

    public GameObject interractInfoPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void ToggleInterractInfo(bool state)
    {
        interractInfoPanel.SetActive(state);
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleInterractInfo += ToggleInterractInfo;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleInterractInfo -= ToggleInterractInfo;
    }
}
