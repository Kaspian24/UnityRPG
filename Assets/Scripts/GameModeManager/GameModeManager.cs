using UnityEngine;
public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public GameObject playerController;

    public GameMode currentGameMode;

    public KeyCode questMenuKey = KeyCode.J;

    public KeyCode inventoryMenuKey = KeyCode.I;

    public KeyCode pauseMenuKey = KeyCode.Escape; // nie zaimplementowane

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
    }

    private void Update()
    {
        switch (currentGameMode)
        {
            case GameMode.Gameplay:
                if (Input.GetKeyDown(questMenuKey))
                {
                    ToggleQuestMenu();
                }
                else if (Input.GetKeyDown(inventoryMenuKey))
                {
                    ToggleInventoryMenu();
                }
                break;
            case GameMode.QuestMenu:
                if (Input.GetKeyDown(questMenuKey))
                {
                    ToggleQuestMenu();
                }
                else if (Input.GetKeyDown(questMenuKey))
                {
                    ToggleQuestMenu();
                    ToggleInventoryMenu();
                }
                break;
            case GameMode.Dialogue:

                break;
            case GameMode.InventoryMenu:
                if (Input.GetKeyDown(inventoryMenuKey))
                {
                    ToggleInventoryMenu();
                }
                else if (Input.GetKeyDown(questMenuKey))
                {
                    ToggleInventoryMenu();
                    ToggleQuestMenu();
                }
                break;
            default:
                break;
        }
    }

    private void ToggleQuestMenu()
    {
        if (currentGameMode == GameMode.QuestMenu)
        {
            currentGameMode = GameMode.Gameplay;
            Resume();
        }
        else
        {
            currentGameMode = GameMode.QuestMenu;
            Pause();
        }
        GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu();
    }

    private void ToggleDialogue()
    {
        if (currentGameMode == GameMode.Dialogue)
        {
            currentGameMode = GameMode.Gameplay;
            Resume();
        }
        else
        {
            currentGameMode = GameMode.Dialogue;
            Pause();
        }
        GameEventsManager.Instance.gameModeEvents.ToggleQuestTrackVisibility();
    }

    private void ToggleInventoryMenu()
    {
        if (currentGameMode == GameMode.InventoryMenu)
        {
            currentGameMode = GameMode.Gameplay;
            Resume();
        }
        else
        {
            currentGameMode = GameMode.InventoryMenu;
            Pause();
        }
        GameEventsManager.Instance.gameModeEvents.ToggleInventory();
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
        GameEventsManager.Instance.gameModeEvents.OnToggleDialogue += ToggleDialogue;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnToggleDialogue -= ToggleDialogue;
    }
}
