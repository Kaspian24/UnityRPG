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
    }

    public void SwitchToLastGameMode()
    {
        (lastGameMode, currentGameMode) = (currentGameMode, lastGameMode);
        switch (currentGameMode)
        {
            case GameMode.Gameplay:
                SwitchToGameplay();
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

    public void ForceResume()
    {
        Resume();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        playerController.GetComponent<FirstPersonController>().enabled = false;
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.GetComponent<FirstPersonController>().enabled = true;
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnDialogueStartEnd += ToggleDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnDialogueStartEnd -= ToggleDialogue;
    }
}
