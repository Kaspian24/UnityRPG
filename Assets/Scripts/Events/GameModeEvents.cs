using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeEvents
{
    public event Action OnToggleQuestMenu;
    public void ToggleQuestMenu()
    {
        OnToggleQuestMenu?.Invoke();
    }
    public event Action OnToggleDialogue;
    public void ToggleDialogue()
    {
        OnToggleDialogue?.Invoke();
    }
    public event Action OnToggleQuestTrackVisibility;
    public void ToggleQuestTrackVisibility()
    {
        OnToggleQuestTrackVisibility?.Invoke();
    }
}
