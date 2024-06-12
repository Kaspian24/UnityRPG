using System;

public class GameModeEvents
{
    public event Action<bool> OnToggleQuestMenu;
    public void ToggleQuestMenu(bool state)
    {
        OnToggleQuestMenu?.Invoke(state);
    }
    public event Action<bool> OnToggleDialogue;
    public void ToggleDialogue(bool state)
    {
        OnToggleDialogue?.Invoke(state);
    }
    public event Action<bool> OnToggleQuestTrackVisibility;
    public void ToggleQuestTrackVisibility(bool state)
    {
        OnToggleQuestTrackVisibility?.Invoke(state);
    }
    public event Action<bool> OnToggleInventory;
    public void ToggleInventory(bool state)
    {
        OnToggleInventory?.Invoke(state);
    }
    public event Action<bool> OnTogglePauseMenu;
    public void TogglePauseMenu(bool state)
    {
        OnTogglePauseMenu?.Invoke(state);
    }
    public event Action<bool> OnToggleMiniMap;
    public void ToggleMiniMap(bool state)
    {
        OnToggleMiniMap?.Invoke(state);
    }

    public event Action<bool> OnDialogueStartEnd;
    public void DialogueStartEnd(bool state)
    {
        OnDialogueStartEnd?.Invoke(state);
    }
}
