using UnityEngine;

/// <summary>
/// Manages quest menu.
/// </summary>
public class QuestMenuManager : MonoBehaviour
{
    public static QuestMenuManager Instance { get; private set; }

    public GameObject questMenuPanel;

    public GameObject trackedQuestPanel;

    public GameObject finishedStartedQuestPanel;

    /// <summary>
    /// Initializes singleton.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Sets panels default states.
    /// </summary>
    private void Start()
    {
        questMenuPanel.SetActive(false);
        if (string.IsNullOrEmpty(GetCurrentlyTrackedQuest()))
        {
            trackedQuestPanel.SetActive(false);
        }
        finishedStartedQuestPanel.SetActive(false);
    }

    /// <summary>
    /// Turns on tracked quest panel, sets tracked quest.
    /// </summary>
    /// <param name="questId">Id of the new quest to track.</param>
    private void TrackQuest(string questId)
    {
        trackedQuestPanel.SetActive(true);
        trackedQuestPanel.GetComponent<TrackedQuestPanel>().TrackQuest(questId);
    }

    /// <summary>
    /// Updates quest track panel.
    /// </summary>
    private void QuestTrackUpdate()
    {
        trackedQuestPanel.SetActive(true);
        trackedQuestPanel.GetComponent<TrackedQuestPanel>().UpdateTracked();
    }

    /// <summary>
    /// Updates quest track panel. Arguments match subscribed event.
    /// </summary>
    /// <param name="questId">Not used</param>
    /// <param name="taskIndex">Not used</param>
    /// <param name="taskData">Not used</param>
    private void QuestTrackUpdate(string questId, int taskIndex, TaskData taskData)
    {
        QuestTrackUpdate();
    }

    /// <summary>
    /// Updates quest track panel. Arguments match subscribed event.
    /// </summary>
    /// <param name="questId">Not used</param>
    private void QuestTrackUpdate(string questId)
    {
        QuestTrackUpdate();
    }

    /// <summary>
    /// Hides tracked quest panel or updates it.
    /// </summary>
    /// <param name="state">Panel state. True to show, false to hide.</param>
    private void QuestTrackUpdate(bool state)
    {
        if (!state)
        {
            trackedQuestPanel.SetActive(false);
        }
        else
        {
            QuestTrackUpdate();
        }
    }

    /// <summary>
    /// Enables finished/started quest panel, forwards quest to the panel.
    /// </summary>
    /// <param name="quest">Quest which state has changed.</param>
    private void QuestFinishedStarted(Quest quest)
    {
        if (QuestManager.Instance.loading)
        {
            return;
        }
        finishedStartedQuestPanel.SetActive(true);
        finishedStartedQuestPanel.GetComponent<FinishedStartedQuestPanel>().AddFinishedStartedQuest(quest);
    }

    /// <summary>
    /// Toggles quest menu panel.
    /// </summary>
    /// <param name="state">Panel state. True to show, false to hide.</param>
    private void ToggleQuestMenu(bool state)
    {
        questMenuPanel.SetActive(state);
    }

    /// <summary>
    /// Getter for currently tracked quest.
    /// </summary>
    /// <returns>QuestId of currently tracked quest.</returns>
    public string GetCurrentlyTrackedQuest()
    {
        return trackedQuestPanel.GetComponent<TrackedQuestPanel>().GetCurrentlyTrackedQuest();
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestTrack += TrackQuest;
        GameEventsManager.Instance.questEvents.OnTaskDataChange += QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnTaskComplete += QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnQuestStateChange += QuestFinishedStarted;

        GameEventsManager.Instance.gameModeEvents.OnToggleQuestMenu += ToggleQuestMenu;
        GameEventsManager.Instance.gameModeEvents.OnToggleQuestTrackVisibility += QuestTrackUpdate;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
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
