using UnityEngine;
using UnityEngine.UI;

public class GameFinishedPanel : MonoBehaviour
{
    public Button button;
    private void Awake()
    {
        button.onClick.AddListener(() => GameModeManager.Instance.SwitchToLastGameMode());
    }
}
