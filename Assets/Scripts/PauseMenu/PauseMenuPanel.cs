using UnityEngine;
using UnityEngine.UI;

public class PauseMenuPanel : MonoBehaviour
{
    public Button resumeButton;

    public Button saveButton;

    public Button loadButton;
    private void Awake()
    {
        resumeButton.onClick.AddListener(() => GameModeManager.Instance.SwitchToLastGameMode());
        saveButton.onClick.AddListener(() => SaveManager.Instance.Save("save1", true));
        loadButton.onClick.AddListener(() => SaveManager.Instance.Load("save1"));
    }
}
