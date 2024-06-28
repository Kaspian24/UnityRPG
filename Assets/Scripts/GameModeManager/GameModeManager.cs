using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages game modes.
/// </summary>
public class GameModeManager : MonoBehaviour
{
    /// <summary>
    /// Enumeration representing different togglable panels.
    /// </summary>
    private enum TogglablePanels
    {
        ToggleQuestMenu,
        ToggleQuestTrackVisibility,
        ToggleDialogue,
        ToggleInventory,
        TogglePauseMenu,
        ToggleMiniMap,
        ToggleLoadMenu,
        ToggleSaveMenu,
        ToggleDeathMessage,
        ToggleGameFinishedMessage,
        ToggleLevelUpMessage,
        ToggleUIBars,
    }
    public static GameModeManager Instance { get; private set; }

    public GameObject playerController;

    public GameMode currentGameMode = GameMode.Gameplay;

    public KeyCode questMenuKey = KeyCode.J;

    public KeyCode inventoryMenuKey = KeyCode.I;

    public KeyCode pauseMenuKey = KeyCode.Escape;

    public GameMode lastGameMode = GameMode.Gameplay;

    private bool playerAlive = true;

    /// <summary>
    /// Initializes singleton.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Changes time scale to 1f.
    /// </summary>
    private void Start()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Reacts on key inputs depending on game mode.
    /// </summary>
    private void Update()
    {
        switch (currentGameMode)
        {
            case GameMode.Gameplay:
                if (Input.GetKeyDown(pauseMenuKey))
                {
                    SwitchToPauseMenu();
                }
                else if (Input.GetKeyDown(questMenuKey))
                {
                    SwitchToQuestMenu();
                }
                else if (Input.GetKeyDown(inventoryMenuKey))
                {
                    SwitchToInventoryMenu();
                }
                break;
            case GameMode.QuestMenu:
                if (Input.GetKeyDown(pauseMenuKey))
                {
                    SwitchToPauseMenu();
                }
                else if (Input.GetKeyDown(questMenuKey))
                {
                    SwitchToGameplay();
                }
                else if (Input.GetKeyDown(inventoryMenuKey))
                {
                    SwitchToInventoryMenu();
                }
                break;
            case GameMode.Dialogue:
                if (Input.GetKeyDown(pauseMenuKey))
                {
                    SwitchToPauseMenu();
                }
                break;
            case GameMode.InventoryMenu:
                if (Input.GetKeyDown(pauseMenuKey))
                {
                    SwitchToPauseMenu();
                }
                else if (Input.GetKeyDown(inventoryMenuKey))
                {
                    SwitchToGameplay();
                }
                else if (Input.GetKeyDown(questMenuKey))
                {
                    SwitchToQuestMenu();
                }
                break;
            case GameMode.PauseMenu:
                if (Input.GetKeyDown(pauseMenuKey))
                {
                    SwitchToLastGameMode();
                }
                break;
            case GameMode.LoadMenu:
                if (Input.GetKeyDown(pauseMenuKey))
                {
                    SwitchToLastGameMode();
                }
                break;
            case GameMode.SaveMenu:
                if (Input.GetKeyDown(pauseMenuKey))
                {
                    SwitchToLastGameMode();
                }
                break;
            case GameMode.GameFinishedMessage:
                if (Input.GetKeyDown(pauseMenuKey))
                {
                    SwitchToLastGameMode();
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Switches to gameplay mode if player is alive.
    /// </summary>
    private void SwitchToGameplay()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.Gameplay;
        Resume();
        TogglePanels(TogglablePanels.ToggleQuestTrackVisibility, TogglablePanels.ToggleMiniMap, TogglablePanels.ToggleUIBars);
    }

    /// <summary>
    /// Switches to quest menu mode if player is alive.
    /// </summary>
    private void SwitchToQuestMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.QuestMenu;
        Pause();
        TogglePanels(TogglablePanels.ToggleQuestMenu);
    }

    /// <summary>
    /// Switches to pause menu mode if player is alive.
    /// </summary>
    private void SwitchToPauseMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.PauseMenu;
        Pause();
        TogglePanels(TogglablePanels.TogglePauseMenu);
    }

    /// <summary>
    /// Switches to dialogue mode if player is alive.
    /// </summary>
    private void SwitchToDialogue()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.Dialogue;
        Pause();
        TogglePanels(TogglablePanels.ToggleDialogue);
    }

    /// <summary>
    /// Switches to inventory mode if player is alive.
    /// </summary>
    private void SwitchToInventoryMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.InventoryMenu;
        Pause();
        TogglePanels(TogglablePanels.ToggleInventory, TogglablePanels.ToggleUIBars);
    }

    /// <summary>
    /// Switches to load menu mode if player is alive.
    /// </summary>
    private void SwitchToLoadMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.LoadMenu;
        Pause();
        TogglePanels(TogglablePanels.ToggleLoadMenu);
    }

    /// <summary>
    /// Switches to save menu mode if player is alive.
    /// </summary>
    private void SwitchToSaveMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.SaveMenu;
        Pause();
        TogglePanels(TogglablePanels.ToggleSaveMenu);
    }

    /// <summary>
    /// Switches to death message mode.
    /// </summary>
    private void SwitchToDeathMessage()
    {
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.Gameplay;
        Pause();
        Time.timeScale = 1f;
        TogglePanels(TogglablePanels.ToggleDeathMessage);
    }

    /// <summary>
    /// Switches to game finished message mode if player is alive. If the last mode was dialogue it doesn't overwrite it.
    /// </summary>
    private void SwitchToGameFinishedMessage()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        if (lastGameMode != GameMode.Dialogue)
        {
            lastGameMode = currentGameMode;
        }
        currentGameMode = GameMode.GameFinishedMessage;
        Pause();
        TogglePanels(TogglablePanels.ToggleGameFinishedMessage);
    }

    /// <summary>
    /// Switches to level up message mode if player is alive.
    /// </summary>
    private void SwitchToLevelUpMessage()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.LevelUpMessage;
        Pause();
        TogglePanels(TogglablePanels.ToggleLevelUpMessage, TogglablePanels.ToggleUIBars);
    }

    /// <summary>
    /// Toggles panels on or off.
    /// </summary>
    /// <param name="panels">Panels to turn on.</param>
    private void TogglePanels(params TogglablePanels[] panels)
    {
        var allPanels = Enum.GetValues(typeof(TogglablePanels)).Cast<TogglablePanels>();
        var panelsSet = new HashSet<TogglablePanels>(panels);

        foreach (TogglablePanels panel in allPanels)
        {
            if (panelsSet.Contains(panel))
            {
                TogglePanel(panel, true);
            }
            else
            {
                TogglePanel(panel, false);
            }
        }
    }

    /// <summary>
    /// Toggles panel on or off.
    /// </summary>
    /// <param name="panel">Panel that will change state.</param>
    /// <param name="state">New state of the panel. True for show, false for hide.</param>
    private void TogglePanel(TogglablePanels panel, bool state)
    {
        switch (panel)
        {
            case TogglablePanels.ToggleQuestMenu:
                GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu(state);
                break;
            case TogglablePanels.ToggleQuestTrackVisibility:
                GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility(state);
                break;
            case TogglablePanels.ToggleDialogue:
                GameEventsManager.Instance.gameModeEvents.ToggleDialogue(state);
                break;
            case TogglablePanels.ToggleInventory:
                GameEventsManager.Instance.gameModeEvents.ToggleInventory(state);
                break;
            case TogglablePanels.TogglePauseMenu:
                GameEventsManager.Instance.gameModeEvents.TogglePauseMenu(state);
                break;
            case TogglablePanels.ToggleMiniMap:
                GameEventsManager.Instance.gameModeEvents.ToggleMiniMap(state);
                break;
            case TogglablePanels.ToggleLoadMenu:
                GameEventsManager.Instance.gameModeEvents.ToggleLoadMenu(state);
                break;
            case TogglablePanels.ToggleSaveMenu:
                GameEventsManager.Instance.gameModeEvents.ToggleSaveMenu(state);
                break;
            case TogglablePanels.ToggleDeathMessage:
                GameEventsManager.Instance.gameModeEvents.ToggleDeathMessage(state);
                break;
            case TogglablePanels.ToggleGameFinishedMessage:
                GameEventsManager.Instance.gameModeEvents.ToggleGameFinishedMessage(state);
                break;
            case TogglablePanels.ToggleLevelUpMessage:
                GameEventsManager.Instance.gameModeEvents.ToggleLevelUpMessage(state);
                break;
            case TogglablePanels.ToggleUIBars:
                GameEventsManager.Instance.gameModeEvents.ToggleUIBars(state);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Switches to last game mode if the player is alive. To prevent loops for most game modes it will switch to gameplay mode instead.
    /// </summary>
    public void SwitchToLastGameMode()
    {
        if (!playerAlive)
        {
            GameEventsManager.Instance.gameModeEvents.ToggleDeathMessage(true);
            return;
        }
        (lastGameMode, currentGameMode) = (currentGameMode, lastGameMode);
        switch (currentGameMode)
        {
            case GameMode.Gameplay:
                if (playerAlive)
                {
                    SwitchToGameplay();
                }
                break;
            case GameMode.QuestMenu:
                SwitchToQuestMenu();
                break;
            case GameMode.Dialogue:
                SwitchToDialogue();
                break;
            case GameMode.InventoryMenu:
                SwitchToInventoryMenu();
                break;
            default:
                SwitchToGameplay();
                break;
        }
    }

    /// <summary>
    /// Toggles dialogue panel.
    /// </summary>
    /// <param name="state">State of the panel. True for show, false for hide.</param>
    private void ToggleDialogue(bool state)
    {
        if (state)
        {
            SwitchToDialogue();
        }
        else
        {
            SwitchToGameplay();
        }
    }

    /// <summary>
    /// Toggles load menu panel.
    /// </summary>
    /// <param name="state">State of the panel. True for show, false for hide.</param>
    private void ToggleLoadMenu(bool state)
    {
        if (state)
        {
            SwitchToLoadMenu();
        }
        else
        {
            SwitchToLastGameMode();
        }
    }

    /// <summary>
    /// Toggles save menu panel.
    /// </summary>
    /// <param name="state">State of the panel. True for show, false for hide.</param>
    private void ToggleSaveMenu(bool state)
    {
        if (state)
        {
            SwitchToSaveMenu();
        }
        else
        {
            SwitchToLastGameMode();
        }
    }

    /// <summary>
    /// Toggles game finished message panel.
    /// </summary>
    /// <param name="state">State of the panel. True for show, false for hide.</param>
    private void ToggleGameFinishedMessage(bool state)
    {
        if (state)
        {
            SwitchToGameFinishedMessage();
        }
        else
        {
            SwitchToLastGameMode();
        }
    }

    /// <summary>
    /// Toggles level up message panel.
    /// </summary>
    /// <param name="state">State of the panel. True for show, false for hide.</param>
    private void ToggleLevelUpMessage(bool state)
    {
        if (state)
        {
            SwitchToLevelUpMessage();
        }
        else
        {
            SwitchToLastGameMode();
        }
    }

    /// <summary>
    /// Resumes game without checking for current game mode.
    /// </summary>
    public void ForceResume()
    {
        Resume();
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameEventsManager.Instance.gameModeEvents.ToggleCrosshair(false);
        GameEventsManager.Instance.gameModeEvents.ToggleInterractInfo(false);
        playerController.GetComponent<FirstPersonController>().enabled = false;
    }

    /// <summary>
    /// Resumes the game.
    /// </summary>
    private void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.GetComponent<FirstPersonController>().enabled = true;
        GameEventsManager.Instance.gameModeEvents.ToggleCrosshair(true);
    }

    /// <summary>
    /// Reacts to player death.
    /// </summary>
    private void Death()
    {
        playerAlive = false;
        SwitchToDeathMessage();
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnDialogueStartEnd += ToggleDialogue;
        GameEventsManager.Instance.gameModeEvents.OnLoadMenuOnOff += ToggleLoadMenu;
        GameEventsManager.Instance.gameModeEvents.OnSaveMenuOnOff += ToggleSaveMenu;
        GameEventsManager.Instance.gameModeEvents.OnDeath += Death;
        GameEventsManager.Instance.gameModeEvents.OnGameFinishedMessageOnOff += ToggleGameFinishedMessage;
        GameEventsManager.Instance.gameModeEvents.OnLevelUpMessageOnOff += ToggleLevelUpMessage;
    }

    /// <summary>
    /// Unsubscribes to events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnDialogueStartEnd -= ToggleDialogue;
        GameEventsManager.Instance.gameModeEvents.OnLoadMenuOnOff -= ToggleLoadMenu;
        GameEventsManager.Instance.gameModeEvents.OnSaveMenuOnOff -= ToggleSaveMenu;
        GameEventsManager.Instance.gameModeEvents.OnDeath -= Death;
        GameEventsManager.Instance.gameModeEvents.OnGameFinishedMessageOnOff -= ToggleGameFinishedMessage;
        GameEventsManager.Instance.gameModeEvents.OnLevelUpMessageOnOff -= ToggleLevelUpMessage;
    }
}
