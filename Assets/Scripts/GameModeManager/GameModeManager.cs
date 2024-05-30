using UnityEngine;
public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public GameObject playerController;

    public GameMode currentGameMode;

    public KeyCode questMenuKey = KeyCode.J;

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
                break;
            case GameMode.QuestMenu:
                if (Input.GetKeyDown(questMenuKey))
                {
                    ToggleQuestMenu();
                }
                break;
            case GameMode.Dialogue:

                break;
            case GameMode.InventoryMenu:

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
            GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu();
        }
        else
        {
            currentGameMode = GameMode.QuestMenu;
            Pause();
            GameEventsManager.Instance.gameModeEvents.ToggleQuestMenu();
        }
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
