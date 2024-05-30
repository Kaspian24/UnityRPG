using UnityEngine;

public class PlaceToVisit : MonoBehaviour
{
    public string placeName;
    private void Awake()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
    }
    private void EnablePlaceToVisit(string placeName)
    {
        if (this.placeName == placeName)
        {
            GetComponent<Collider>().enabled = true;
        }

    }
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnEnablePlaceToVisit += EnablePlaceToVisit;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnEnablePlaceToVisit -= EnablePlaceToVisit;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameEventsManager.Instance.questEvents.PlaceVisited(placeName);
        GetComponent<Collider>().enabled = false;
    }
}
