using UnityEngine;

public class QuestMenuManager : MonoBehaviour
{
    public static QuestMenuManager Instance { get; private set; }

    bool paused;

    public GameObject questMenuPanel;

    public GameObject playerController;

    public GameObject trackedQuestPanel;

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

    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestTrack += TrackQuest;
        GameEventsManager.Instance.questEvents.OnQuestTrackUpdate += QuestTrackUpdate;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestTrack -= TrackQuest;
        GameEventsManager.Instance.questEvents.OnQuestTrackUpdate -= QuestTrackUpdate;
    }
}
