using UnityEngine;

/// <summary>
/// Manages events.
/// </summary>
public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public QuestEvents questEvents;

    public DialogueEvents dialogueEvents;

    public GameModeEvents gameModeEvents;

    public PlayerEvents playerEvents;

    /// <summary>
    /// Singleton initialization.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        questEvents = new QuestEvents();

        dialogueEvents = new DialogueEvents();

        gameModeEvents = new GameModeEvents();

        playerEvents = new PlayerEvents();
    }
}
