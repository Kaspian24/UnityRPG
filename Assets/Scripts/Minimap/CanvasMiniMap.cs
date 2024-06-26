using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMiniMap : MonoBehaviour
{
    public GameObject minimapPanel;
    public GameObject manaBarPanel;
    public GameObject healthBarPanel;
    private void ToggleMinimap(bool state)
    {
        minimapPanel.SetActive(state);
        manaBarPanel.SetActive(state);
        healthBarPanel.SetActive(state);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleMiniMap += ToggleMinimap;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleMiniMap -= ToggleMinimap;
    }
}
