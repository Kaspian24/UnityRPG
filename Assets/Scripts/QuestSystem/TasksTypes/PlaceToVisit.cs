using UnityEngine;

/// <summary>
/// Trigger for place to visit task.
/// </summary>
public class PlaceToVisit : MonoBehaviour
{
    public string placeName;

    /// <summary>
    /// Sets collider to disabled and specyfies it as a trigger.
    /// </summary>
    private void Awake()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
    }

    /// <summary>
    /// Enables collider if the place names match.
    /// </summary>
    /// <param name="placeName">Name of the place.</param>
    private void EnablePlaceToVisit(string placeName)
    {
        if (this.placeName == placeName)
        {
            GetComponent<Collider>().enabled = true;
        }

    }

    /// <summary>
    /// Subscribes to enable place to visit event.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnEnablePlaceToVisit += EnablePlaceToVisit;
    }

    /// <summary>
    /// Unsubscribes from enable place to visit event.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnEnablePlaceToVisit -= EnablePlaceToVisit;
    }

    /// <summary>
    /// Triggers place visited event if collided with player.
    /// </summary>
    /// <param name="other">Collider of the second object.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        GameEventsManager.Instance.questEvents.PlaceVisited(placeName);
        GetComponent<Collider>().enabled = false;
    }
}
