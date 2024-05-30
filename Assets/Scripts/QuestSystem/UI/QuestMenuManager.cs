using UnityEngine;

public class QuestMenuManager : MonoBehaviour
{
    public static QuestMenuManager Instance { get; private set; }

    bool paused;

    public GameObject questMenuPanel;

    public GameObject playerController;

    public GameObject trackedQuestPanel;

    public GameObject finishedQuestPanel;

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
        finishedQuestPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Pause() // to powinno byæ w osobnym menagerze stanu gry
    {
        paused = true;
        questMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        playerController.GetComponent<FirstPersonController>().enabled = false;
    }

    private void Resume() // to powinno byæ w osobnym menagerze stanu gry
    {
        paused = false;
        questMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        playerController.GetComponent<FirstPersonController>().enabled = true;
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

    private void QuestFinished(Quest quest)
    {
        finishedQuestPanel.SetActive(true);
        finishedQuestPanel.GetComponent<FinishedQuestPanel>().AddFinishedQuest(quest);
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestTrack += TrackQuest;
        GameEventsManager.Instance.questEvents.OnTaskDataChange += QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnTaskComplete += QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnQuestStateChange += QuestFinished;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestTrack -= TrackQuest;
        GameEventsManager.Instance.questEvents.OnTaskDataChange -= QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnTaskComplete -= QuestTrackUpdate;
        GameEventsManager.Instance.questEvents.OnQuestStateChange -= QuestFinished;
    }
}
