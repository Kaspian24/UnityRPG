using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public QuestEvents questEvents;

    public DialogueEventManager dialogueEventManager;

    private void Awake()
    {
        Instance = this;

        questEvents = new QuestEvents();

        dialogueEventManager = new DialogueEventManager();
    }
}
