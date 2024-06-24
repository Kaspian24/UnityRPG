using UnityEngine;

public class QuestStartTrigger : MonoBehaviour
{
    public string questId;
    private void Awake()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Collider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (QuestManager.Instance.GetQuestById(questId).state == QuestState.CanStart)
        {
            GameEventsManager.Instance.questEvents.QuestStart(questId);
            GetComponent<Collider>().enabled = false;
        }

    }
}
