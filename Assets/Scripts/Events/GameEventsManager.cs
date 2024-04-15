using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public QuestEvents questEvents;

    private void Awake()
    {
        Instance = this;

        questEvents = new QuestEvents();
    }
}
