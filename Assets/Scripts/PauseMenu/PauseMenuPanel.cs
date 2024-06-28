using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlls pause menu panel.
/// </summary>
public class PauseMenuPanel : MonoBehaviour
{
    public Button resumeButton;

    public Button loadButton;

    public Button mainMenuButton;

    /// <summary>
    /// Adds listeners to buttons.
    /// </summary>
    private void Awake()
    {
        resumeButton.onClick.AddListener(() => GameModeManager.Instance.SwitchToLastGameMode());
        loadButton.onClick.AddListener(() => GameEventsManager.Instance.gameModeEvents.LoadMenuOnOff(true));
        mainMenuButton.onClick.AddListener(() => SaveManager.Instance.MainMenu());
    }
}
