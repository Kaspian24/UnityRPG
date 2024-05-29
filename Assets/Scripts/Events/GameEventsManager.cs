using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public QuestEvents questEvents;

    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        Instance = this;

        questEvents = new QuestEvents();

        dialogueEvents = new DialogueEvents();
    }
}
