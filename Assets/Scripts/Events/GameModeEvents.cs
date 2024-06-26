using System;
using System.Diagnostics;

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
    public event Action<bool> OnToggleLoadMenu;
    public void ToggleLoadMenu(bool state)
    {
        OnToggleLoadMenu?.Invoke(state);
    }
    public event Action<bool> OnToggleSaveMenu;
    public void ToggleSaveMenu(bool state)
    {
        OnToggleSaveMenu?.Invoke(state);
    }
    public event Action<bool> OnToggleCrosshair;
    public void ToggleCrosshair(bool state)
    {
        OnToggleCrosshair?.Invoke(state);
    }
    public event Action<bool> OnToggleInterractInfo;
    public void ToggleInterractInfo(bool state)
    {
        OnToggleInterractInfo?.Invoke(state);
    }
    public event Action<bool> OnToggleDeathMessage;
    public void ToggleDeathMessage(bool state)
    {
        OnToggleDeathMessage?.Invoke(state);
    }
    public event Action<bool> OnToggleGameFinishedMessage;
    public void ToggleGameFinishedMessage(bool state)
    {
        OnToggleGameFinishedMessage?.Invoke(state);
    }
    public event Action<bool> OnToggleLevelUpMessage;
    public void ToggleLevelUpMessage(bool state)
    {
        OnToggleLevelUpMessage?.Invoke(state);
    }

    public event Action<bool> OnDialogueStartEnd;
    public void DialogueStartEnd(bool state)
    {
        OnDialogueStartEnd?.Invoke(state);
    }

    public event Action<bool> OnLoadMenuOnOff;
    public void LoadMenuOnOff(bool state)
    {
        OnLoadMenuOnOff?.Invoke(state);
    }

    public event Action<bool> OnSaveMenuOnOff;
    public void SaveMenuOnOff(bool state)
    {
        OnSaveMenuOnOff?.Invoke(state);
    }

    public event Action OnReloadSaveMenu;
    public void ReloadSaveMenu()
    {
        OnReloadSaveMenu?.Invoke();
    }

    public event Action OnReloadLoadMenu;
    public void ReloadLoadMenu()
    {
        OnReloadLoadMenu?.Invoke();
    }

    public event Action OnDeath;
    public void Death()
    {
        OnDeath?.Invoke();
    }

    public event Action<bool> OnGameFinishedMessageOnOff;
    public void GameFinishedMessageOnOff(bool state)
    {
        OnGameFinishedMessageOnOff?.Invoke(state);
    }

    public event Action<bool> OnLevelUpMessageOnOff;
    public void LevelUpMessageOnOff(bool state)
    {
        OnLevelUpMessageOnOff?.Invoke(state);
    }
}
