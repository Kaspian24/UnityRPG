using UnityEngine;

public class QuestMenuManager : MonoBehaviour
{
    public static QuestMenuManager Instance { get; private set; }

    public GameObject questMenuPanel;

    public GameObject trackedQuestPanel;

    public GameObject finishedStartedQuestPanel;

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
        questMenuPanel.SetActive(false);
        trackedQuestPanel.SetActive(false);
        finishedStartedQuestPanel.SetActive(false);
    }

    private void TrackQuest(string questId)
    {
        trackedQuestPanel.SetActive(true);
        trackedQuestPanel.GetComponent<TrackedQuestPanel>().TrackQuest(questId);
    }
    private void QuestTrackUpdate()
    {
        trackedQuestPanel.SetActive(true);
        trackedQuestPanel.GetComponent<TrackedQuestPanel>().UpdateTracked();
    }
    private void QuestTrackUpdate(string questId, int taskIndex, TaskData taskData)
    {
        QuestTrackUpdate();
    }
    private void QuestTrackUpdate(string questId)
    {
        QuestTrackUpdate();
    }
    private void QuestFinishedStarted(Quest quest)
    {
        finishedStartedQuestPanel.SetActive(true);
        finishedStartedQuestPanel.GetComponent<FinishedStartedQuestPanel>().AddFinishedStartedQuest(quest);
    }

    private void ToggleQuestMenu()
    {
        questMenuPanel.SetActive(!questMenuPanel.activeInHierarchy);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestTrack += TrackQuest;
        GameEventsManager.Instance.questEvents.OnTaskDataChange += QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnTaskComplete += QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnQuestStateChange += QuestFinishedStarted;

        GameEventsManager.Instance.gameModeEvents.OnToggleQuestMenu += ToggleQuestMenu;
        GameEventsManager.Instance.gameModeEvents.OnToggleQuestTrackVisibility += QuestTrackUpdate;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestTrack -= TrackQuest;
        GameEventsManager.Instance.questEvents.OnTaskDataChange -= QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnTaskComplete -= QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnQuestStateChange -= QuestFinishedStarted;

        GameEventsManager.Instance.gameModeEvents.OnToggleQuestMenu -= ToggleQuestMenu;
        GameEventsManager.Instance.gameModeEvents.OnToggleQuestTrackVisibility -= QuestTrackUpdate;
    }
}
