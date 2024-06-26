using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameModeManager : MonoBehaviour
{
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
    }
    public static GameModeManager Instance { get; private set; }

    public GameObject playerController;

    public GameMode currentGameMode = GameMode.Gameplay;

    public KeyCode questMenuKey = KeyCode.J;

    public KeyCode inventoryMenuKey = KeyCode.I;

    public KeyCode pauseMenuKey = KeyCode.Escape;

    public GameMode lastGameMode = GameMode.Gameplay;

    private bool playerAlive = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

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

    private void SwitchToGameplay()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        currentGameMode = GameMode.Gameplay;
        Resume();
        TogglePanels(TogglablePanels.ToggleQuestTrackVisibility, TogglablePanels.ToggleMiniMap);
    }

    private void SwitchToQuestMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        currentGameMode = GameMode.QuestMenu;
        Pause();
        TogglePanels(TogglablePanels.ToggleQuestMenu);
    }

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

    private void SwitchToDialogue()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        currentGameMode = GameMode.Dialogue;
        Pause();
        TogglePanels(TogglablePanels.ToggleDialogue);
    }

    private void SwitchToInventoryMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        currentGameMode = GameMode.InventoryMenu;
        Pause();
        TogglePanels(TogglablePanels.ToggleInventory);
    }

    private void SwitchToLoadMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        currentGameMode = GameMode.LoadMenu;
        Pause();
        TogglePanels(TogglablePanels.ToggleLoadMenu);
    }

    private void SwitchToSaveMenu()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        currentGameMode = GameMode.SaveMenu;
        Pause();
        TogglePanels(TogglablePanels.ToggleSaveMenu);
    }

    private void SwitchToDeathMessage()
    {
        currentGameMode = GameMode.Gameplay;
        Pause();
        Time.timeScale = 1f;
        TogglePanels(TogglablePanels.ToggleDeathMessage);
    }

    private void SwitchToGameFinishedMessage()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.GameFinishedMessage;
        Pause();
        TogglePanels(TogglablePanels.ToggleGameFinishedMessage);
    }

    private void SwitchToLevelUpMessage()
    {
        if (!playerAlive)
        {
            SwitchToDeathMessage();
            return;
        }
        currentGameMode = GameMode.LevelUpMessage;
        Pause();
        TogglePanels(TogglablePanels.ToggleLevelUpMessage);
    }

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

    private void TogglePanel (TogglablePanels panel, bool state)
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
            default:
                break;
        }
    }

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
            case GameMode.PauseMenu:
                SwitchToGameplay();
                break;
            case GameMode.LoadMenu:
                SwitchToGameplay();
                break;
            case GameMode.SaveMenu:
                SwitchToGameplay();
                break;
            case GameMode.GameFinishedMessage:
                SwitchToGameplay();
                break;
            default:
                break;
        }
    }

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

    public void ForceResume()
    {
        Resume();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameEventsManager.Instance.gameModeEvents.ToggleCrosshair(false);
        GameEventsManager.Instance.gameModeEvents.ToggleInterractInfo(false);
        playerController.GetComponent<FirstPersonController>().enabled = false;
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.GetComponent<FirstPersonController>().enabled = true;
        GameEventsManager.Instance.gameModeEvents.ToggleCrosshair(true);
    }

    private void Death()
    {
        playerAlive = false;
        SwitchToDeathMessage();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnDialogueStartEnd += ToggleDialogue;
        GameEventsManager.Instance.gameModeEvents.OnLoadMenuOnOff += ToggleLoadMenu;
        GameEventsManager.Instance.gameModeEvents.OnSaveMenuOnOff += ToggleSaveMenu;
        GameEventsManager.Instance.gameModeEvents.OnDeath += Death;
        GameEventsManager.Instance.gameModeEvents.OnGameFinishedMessageOnOff += ToggleGameFinishedMessage;
        GameEventsManager.Instance.gameModeEvents.OnLevelUpMessageOnOff += ToggleLevelUpMessage;
    }

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
