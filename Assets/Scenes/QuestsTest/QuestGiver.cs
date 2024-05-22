using UnityEngine;

[System.Serializable]
public class QuestGiver : MonoBehaviour
{
    [Header("Ink Json Dialogue")]
    [SerializeField]
    private TextAsset inkJson;

    public QuestStaticSO questSO;
    private string questId;
    private void Awake()
    {
        questId = questSO.Id;
        GetComponent<Collider>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStateChange -= QuestStateChange;
    }
    private void QuestStateChange(Quest quest)
    {
        if (questId == quest.staticData.Id && quest.state == QuestState.CanStart)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            return;
        }
        if (inkJson)
        {
            DialogueManager.Instance.StartDialogue(inkJson);
        }
        else
        {
            GameEventsManager.Instance.questEvents.QuestStart(questId);
            Destroy(gameObject);
        }
    }
}
