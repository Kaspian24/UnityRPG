using UnityEngine;
using UnityEngine.UI;

public class PauseMenuPanel : MonoBehaviour
{
    public Button resumeButton;

    public Button saveButton;

    public Button loadButton;

    public Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => GameModeManager.Instance.SwitchToLastGameMode());
        saveButton.onClick.AddListener(() => GameEventsManager.Instance.gameModeEvents.SaveMenuOnOff(true));
        loadButton.onClick.AddListener(() => GameEventsManager.Instance.gameModeEvents.LoadMenuOnOff(true));
        mainMenuButton.onClick.AddListener(() => SaveManager.Instance.MainMenu());
    }
}
