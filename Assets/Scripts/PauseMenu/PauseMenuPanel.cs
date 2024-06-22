using UnityEngine;
using UnityEngine.UI;

public class PauseMenuPanel : MonoBehaviour
{
    public Button resumeButton;

    public Button loadButton;

    public Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => GameModeManager.Instance.SwitchToLastGameMode());
        loadButton.onClick.AddListener(() => GameEventsManager.Instance.gameModeEvents.LoadMenuOnOff(true));
        mainMenuButton.onClick.AddListener(() => SaveManager.Instance.MainMenu());
    }
}
