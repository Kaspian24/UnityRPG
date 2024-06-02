using System;

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
    public event Action OnToggleInventory;
    public void ToggleInventory()
    {
        OnToggleInventory?.Invoke();
    }
    public event Action OnTogglePauseMenu;
    public void TogglePauseMenu()
    {
        OnTogglePauseMenu?.Invoke();
    }
}
