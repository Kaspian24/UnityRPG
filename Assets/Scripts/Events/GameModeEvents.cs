using System;

/// <summary>
/// Class with events related to game mode.
/// </summary>
public class GameModeEvents
{
    /// <summary>
    /// Triggered when quest menu is opened.
    /// </summary>
    public event Action<bool> OnToggleQuestMenu;

    /// <summary>
    /// Invokes <see cref="OnToggleQuestMenu"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleQuestMenu(bool state)
    {
        OnToggleQuestMenu?.Invoke(state);
    }

    /// <summary>
    /// Triggered when dialogue menu is opened.
    /// </summary>
    public event Action<bool> OnToggleDialogue;

    /// <summary>
    /// Invokes <see cref="OnToggleDialogue"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleDialogue(bool state)
    {
        OnToggleDialogue?.Invoke(state);
    }

    /// <summary>
    /// Triggered when quest track panel gets visible.
    /// </summary>
    public event Action<bool> OnToggleQuestTrackVisibility;

    /// <summary>
    /// Invokes <see cref="OnToggleQuestTrackVisibility"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleQuestTrackVisibility(bool state)
    {
        OnToggleQuestTrackVisibility?.Invoke(state);
    }

    /// <summary>
    /// Triggered when inventory is opened.
    /// </summary>
    public event Action<bool> OnToggleInventory;

    /// <summary>
    /// Invokes <see cref="OnToggleInventory"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleInventory(bool state)
    {
        OnToggleInventory?.Invoke(state);
    }

    /// <summary>
    /// Triggered when pause menu is opened.
    /// </summary>
    public event Action<bool> OnTogglePauseMenu;

    /// <summary>
    /// Invokes <see cref="OnTogglePauseMenu"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void TogglePauseMenu(bool state)
    {
        OnTogglePauseMenu?.Invoke(state);
    }

    /// <summary>
    /// Triggered when minimap gets visible.
    /// </summary>
    public event Action<bool> OnToggleMiniMap;

    /// <summary>
    /// Invokes <see cref="OnToggleMiniMap"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleMiniMap(bool state)
    {
        OnToggleMiniMap?.Invoke(state);
    }

    /// <summary>
    /// Triggered when load menu is opened.
    /// </summary>
    public event Action<bool> OnToggleLoadMenu;

    /// <summary>
    /// Invokes <see cref="OnToggleLoadMenu"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleLoadMenu(bool state)
    {
        OnToggleLoadMenu?.Invoke(state);
    }

    /// <summary>
    /// Triggered when save menu is opened.
    /// </summary>
    public event Action<bool> OnToggleSaveMenu;

    /// <summary>
    /// Invokes <see cref="OnToggleSaveMenu"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleSaveMenu(bool state)
    {
        OnToggleSaveMenu?.Invoke(state);
    }

    /// <summary>
    /// Triggered when crosshair gets visible.
    /// </summary>
    public event Action<bool> OnToggleCrosshair;

    /// <summary>
    /// Invokes <see cref="OnToggleCrosshair"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleCrosshair(bool state)
    {
        OnToggleCrosshair?.Invoke(state);
    }

    /// <summary>
    /// Triggered when interract info gets visible.
    /// </summary>
    public event Action<bool> OnToggleInterractInfo;

    /// <summary>
    /// Invokes <see cref="OnToggleInterractInfo"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleInterractInfo(bool state)
    {
        OnToggleInterractInfo?.Invoke(state);
    }

    /// <summary>
    /// Triggered when death message gets visible.
    /// </summary>
    public event Action<bool> OnToggleDeathMessage;

    /// <summary>
    /// Invokes <see cref="OnToggleDeathMessage"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleDeathMessage(bool state)
    {
        OnToggleDeathMessage?.Invoke(state);
    }

    /// <summary>
    /// Triggered when game finished message gets visible.
    /// </summary>
    public event Action<bool> OnToggleGameFinishedMessage;

    /// <summary>
    /// Invokes <see cref="OnToggleGameFinishedMessage"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleGameFinishedMessage(bool state)
    {
        OnToggleGameFinishedMessage?.Invoke(state);
    }

    /// <summary>
    /// Triggered when level up message gets visible.
    /// </summary>
    public event Action<bool> OnToggleLevelUpMessage;

    /// <summary>
    /// Invokes <see cref="OnToggleLevelUpMessage"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleLevelUpMessage(bool state)
    {
        OnToggleLevelUpMessage?.Invoke(state);
    }

    /// <summary>
    /// Triggered when bars from UI get visible.
    /// </summary>
    public event Action<bool> OnToggleUIBars;

    /// <summary>
    /// Invokes <see cref="OnToggleUIBars"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void ToggleUIBars(bool state)
    {
        OnToggleUIBars?.Invoke(state);
    }

    /// <summary>
    /// Triggers when dialogue panel should be shown.
    /// </summary>
    public event Action<bool> OnDialogueStartEnd;

    /// <summary>
    /// Invokes <see cref="OnDialogueStartEnd"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void DialogueStartEnd(bool state)
    {
        OnDialogueStartEnd?.Invoke(state);
    }

    /// <summary>
    /// Triggers when load menu should be shown.
    /// </summary>
    public event Action<bool> OnLoadMenuOnOff;

    /// <summary>
    /// Invokes <see cref="OnLoadMenuOnOff"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void LoadMenuOnOff(bool state)
    {
        OnLoadMenuOnOff?.Invoke(state);
    }

    /// <summary>
    /// Triggers when save menu should be shown.
    /// </summary>
    public event Action<bool> OnSaveMenuOnOff;

    /// <summary>
    /// Invokes <see cref="OnSaveMenuOnOff"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void SaveMenuOnOff(bool state)
    {
        OnSaveMenuOnOff?.Invoke(state);
    }

    /// <summary>
    /// Triggers when save menu gets reloaded.
    /// </summary>
    public event Action OnReloadSaveMenu;

    /// <summary>
    /// Invokes <see cref="OnReloadSaveMenu"/> event.
    /// </summary>
    public void ReloadSaveMenu()
    {
        OnReloadSaveMenu?.Invoke();
    }

    /// <summary>
    /// Triggers when load menu gets reloaded.
    /// </summary>
    public event Action OnReloadLoadMenu;

    /// <summary>
    /// Invokes <see cref="OnReloadLoadMenu"/> event.
    /// </summary>
    public void ReloadLoadMenu()
    {
        OnReloadLoadMenu?.Invoke();
    }

    /// <summary>
    /// Triggers when player dies.
    /// </summary>
    public event Action OnDeath;

    /// <summary>
    /// Invokes <see cref="OnDeath"/> event.
    /// </summary>
    public void Death()
    {
        OnDeath?.Invoke();
    }

    /// <summary>
    /// Triggers when game finished message should get visible.
    /// </summary>
    public event Action<bool> OnGameFinishedMessageOnOff;

    /// <summary>
    /// Invokes <see cref="OnGameFinishedMessageOnOff"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void GameFinishedMessageOnOff(bool state)
    {
        OnGameFinishedMessageOnOff?.Invoke(state);
    }

    /// <summary>
    /// Triggers when level up message should get visible.
    /// </summary>
    public event Action<bool> OnLevelUpMessageOnOff;

    /// <summary>
    /// Invokes <see cref="OnLevelUpMessageOnOff"/> event.
    /// </summary>
    /// <param name="state">State of UI element. True on show, false on hide.</param>
    public void LevelUpMessageOnOff(bool state)
    {
        OnLevelUpMessageOnOff?.Invoke(state);
    }
}
