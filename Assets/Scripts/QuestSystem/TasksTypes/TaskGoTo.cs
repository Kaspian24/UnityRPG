using UnityEngine;

[System.Serializable]
public class TaskGoToData
{
    public string placeName;
    [Header("PÛjdü do {displayedPlace}.")]
    public string displayedPlaceInSentence;
}

public class TaskGoTo : Task
{
    public TaskGoToData[] placesToVisit;
    private bool[] placesVisited;

    private void PlaceVisited(string placeName)
    {
        for (int i = 0; i < placesToVisit.Length; i++)
        {
            if (placesToVisit[i].placeName == placeName)
            {
                placesVisited[i] = true;
                UpdateState();
                return;
            }
        }
    }

    private void Awake()
    {
        placesVisited = new bool[placesToVisit.Length];
        foreach (TaskGoToData place in placesToVisit)
        {
            if (string.IsNullOrEmpty(place.displayedPlaceInSentence))
            {
                place.displayedPlaceInSentence = place.placeName;
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < placesToVisit.Length; i++)
        {
            string placeName = placesToVisit[i].placeName;
            if (!placesVisited[i])
            {
                GameEventsManager.Instance.questEvents.EnablePlaceToVisit(placeName);
            }
        }
        UpdateState();
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnPlaceVisited += PlaceVisited;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnPlaceVisited -= PlaceVisited;
    }
    private void UpdateState()
    {
        string state = JsonUtility.ToJson(placesVisited);
        (string, bool)[] log = new (string, bool)[placesToVisit.Length];
        bool completed = true;
        for (int i = 0; i < log.Length; i++)
        {
            string placeName = placesToVisit[i].displayedPlaceInSentence;
            bool stepFinished = placesVisited[i];
            log[i] = ($"PÛjdü do {placeName}.", stepFinished);
            if (!stepFinished)
            {
                completed = false;
            }
        }
        ChangeData(state, log);
        if (completed)
        {
            Complete();
        }
    }
    protected override void SetTaskState(string state)
    {
        placesVisited = JsonUtility.FromJson<bool[]>(state);
        UpdateState();
    }
}
