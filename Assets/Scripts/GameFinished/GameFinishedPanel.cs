using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlls game finished message.
/// </summary>
public class GameFinishedPanel : MonoBehaviour
{
    public Button button;

    /// <summary>
    /// Links go back button with function to switch to the last game mode.
    /// </summary>
    private void Awake()
    {
        button.onClick.AddListener(() => GameModeManager.Instance.SwitchToLastGameMode());
    }
}
