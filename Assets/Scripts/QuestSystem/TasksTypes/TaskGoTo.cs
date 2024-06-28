using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// TaskGoTo data.
/// </summary>
[System.Serializable]
public class TaskGoToData
{
    public string placeName;
    [Header("PÛjdü do {displayedPlace}.")]
    public string displayedPlaceInSentence;
}

/// <summary>
/// Go to a place task type.
/// </summary>
public class TaskGoTo : Task
{
    public TaskGoToData[] placesToVisit;
    private bool[] placesVisited;

    /// <summary>
    /// Toggles place to visited if the place name matches with one from the array.
    /// </summary>
    /// <param name="placeName">Name of the place.</param>
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

    /// <summary>
    /// Initializes placesVisited array, overwrites not specified displayed place names with default names.
    /// </summary>
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

    /// <summary>
    /// Enables triggers for not yet visited places. Calls update state on start.
    /// </summary>
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

    /// <summary>
    /// Subscribes to place visited event.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnPlaceVisited += PlaceVisited;
    }

    /// <summary>
    /// Unsubscribes from place visited event.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnPlaceVisited -= PlaceVisited;
    }

    /// <summary>
    /// Updates task state.
    /// </summary>
    private void UpdateState()
    {
        string state = JsonConvert.SerializeObject(placesVisited);
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

    /// <inheritdoc/>
    protected override void SetTaskState(string state)
    {
        placesVisited = JsonConvert.DeserializeObject<bool[]>(state);
        UpdateState();
    }
}
