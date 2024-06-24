using UnityEngine;
public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public GameObject playerController;

    public GameMode currentGameMode;

    public KeyCode questMenuKey = KeyCode.J;

    public KeyCode inventoryMenuKey = KeyCode.I;

    public KeyCode pauseMenuKey = KeyCode.Escape;

    public GameMode lastGameMode;

    public bool playerAlive;

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
        currentGameMode = GameMode.Gameplay;
        lastGameMode = GameMode.Gameplay;
        playerAlive = true;
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
            default:
                break;
        }
    }

    private void SwitchToGameplay()
    {
        currentGameMode = GameMode.Gameplay;
        Resume();
        GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility(true);
        GameEventsManager.Instance.gameModeEvents.ToggleDialogue(false);
        GameEventsManager.Instance.gameModeEvents.ToggleInventory(false);
        GameEventsManager.Instance.gameModeEvents.TogglePauseMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleMiniMap(true);
        GameEventsManager.Instance.gameModeEvents.ToggleLoadMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleSaveMenu(false);
    }

    private void SwitchToQuestMenu()
    {
        currentGameMode = GameMode.QuestMenu;
        Pause();
        GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu(true);
        GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility(false);
        GameEventsManager.Instance.gameModeEvents.ToggleDialogue(false);
        GameEventsManager.Instance.gameModeEvents.ToggleInventory(false);
        GameEventsManager.Instance.gameModeEvents.TogglePauseMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleMiniMap(false);
        GameEventsManager.Instance.gameModeEvents.ToggleLoadMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleSaveMenu(false);
    }

    private void SwitchToPauseMenu()
    {
        lastGameMode = currentGameMode;
        currentGameMode = GameMode.PauseMenu;
        Pause();
        GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility(false);
        GameEventsManager.Instance.gameModeEvents.ToggleDialogue(false);
        GameEventsManager.Instance.gameModeEvents.ToggleInventory(false);
        GameEventsManager.Instance.gameModeEvents.TogglePauseMenu(true);
        GameEventsManager.Instance.gameModeEvents.ToggleMiniMap(false);
        GameEventsManager.Instance.gameModeEvents.ToggleLoadMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleSaveMenu(false);
    }

    private void SwitchToDialogue()
    {
        currentGameMode = GameMode.Dialogue;
        Pause();
        GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility(false);
        GameEventsManager.Instance.gameModeEvents.ToggleDialogue(true);
        GameEventsManager.Instance.gameModeEvents.ToggleInventory(false);
        GameEventsManager.Instance.gameModeEvents.TogglePauseMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleMiniMap(false);
        GameEventsManager.Instance.gameModeEvents.ToggleLoadMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleSaveMenu(false);
    }

    private void SwitchToInventoryMenu()
    {
        currentGameMode = GameMode.InventoryMenu;
        Pause();
        GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility(false);
        GameEventsManager.Instance.gameModeEvents.ToggleDialogue(false);
        GameEventsManager.Instance.gameModeEvents.ToggleInventory(true);
        GameEventsManager.Instance.gameModeEvents.TogglePauseMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleMiniMap(false);
        GameEventsManager.Instance.gameModeEvents.ToggleLoadMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleSaveMenu(false);
    }

    private void SwitchToLoadMenu()
    {
        currentGameMode = GameMode.LoadMenu;
        Pause();
        GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility(false);
        GameEventsManager.Instance.gameModeEvents.ToggleDialogue(false);
        GameEventsManager.Instance.gameModeEvents.ToggleInventory(false);
        GameEventsManager.Instance.gameModeEvents.TogglePauseMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleMiniMap(false);
        GameEventsManager.Instance.gameModeEvents.ToggleLoadMenu(true);
        GameEventsManager.Instance.gameModeEvents.ToggleSaveMenu(false);
    }

    private void SwitchToSaveMenu()
    {
        currentGameMode = GameMode.SaveMenu;
        Pause();
        GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility(false);
        GameEventsManager.Instance.gameModeEvents.ToggleDialogue(false);
        GameEventsManager.Instance.gameModeEvents.ToggleInventory(false);
        GameEventsManager.Instance.gameModeEvents.TogglePauseMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleMiniMap(false);
        GameEventsManager.Instance.gameModeEvents.ToggleLoadMenu(false);
        GameEventsManager.Instance.gameModeEvents.ToggleSaveMenu(true);
    }

    public void SwitchToLastGameMode()
    {
        (lastGameMode, currentGameMode) = (currentGameMode, lastGameMode);
        switch (currentGameMode)
        {
            case GameMode.Gameplay:
                if(playerAlive)
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

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnDialogueStartEnd += ToggleDialogue;
        GameEventsManager.Instance.gameModeEvents.OnLoadMenuOnOff += ToggleLoadMenu;
        GameEventsManager.Instance.gameModeEvents.OnSaveMenuOnOff += ToggleSaveMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnDialogueStartEnd -= ToggleDialogue;
        GameEventsManager.Instance.gameModeEvents.OnLoadMenuOnOff -= ToggleLoadMenu;
        GameEventsManager.Instance.gameModeEvents.OnSaveMenuOnOff -= ToggleSaveMenu;
    }
}
