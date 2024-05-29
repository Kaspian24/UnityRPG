using UnityEngine;

[System.Serializable]
public class TaskTalkData
{
    public string npcName;
    public string conversationTopic;
    [Header("Porozmawiaj z {displayedName} o {displayedTopic}.")]
    public string displayedNameInSentence;
    public string displayedTopicInSentence;
}
public class TaskTalk : Task
{
    public TaskTalkData[] npcsToTalkTo;
    private bool[] npcsTalkedTo;

    private void TopicTalkedAbout(string npcName, string conversationTopic)
    {
        for (int i = 0; i < npcsToTalkTo.Length; i++)
        {
            if (npcsToTalkTo[i].conversationTopic == conversationTopic && npcsToTalkTo[i].npcName == npcName)
            {
                npcsTalkedTo[i] = true;
                UpdateState();
                return;
            }
        }
    }

    private void Awake()
    {
        npcsTalkedTo = new bool[npcsToTalkTo.Length];
        foreach (TaskTalkData npc in npcsToTalkTo)
        {
            if (string.IsNullOrEmpty(npc.displayedNameInSentence))
            {
                npc.displayedNameInSentence = npc.npcName;
            }
            if (string.IsNullOrEmpty(npc.displayedTopicInSentence))
            {
                npc.displayedTopicInSentence = npc.conversationTopic;
            }
        }
    }

    private void Start()
    {
        for(int i = 0; i < npcsToTalkTo.Length; i++)
        {
            string npcName = npcsToTalkTo[i].npcName;
            string conversationTopic = npcsToTalkTo[i].conversationTopic;
            if (!npcsTalkedTo[i])
            {
                GameEventsManager.Instance.dialogueEvents.EnableTopic(npcName, conversationTopic);
            }
        }
        UpdateState();
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.OnTopicTalkedAbout += TopicTalkedAbout;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.OnTopicTalkedAbout -= TopicTalkedAbout;
    }
    private void UpdateState()
    {
        string state = JsonUtility.ToJson(npcsTalkedTo);
        (string, bool)[] log = new (string, bool)[npcsToTalkTo.Length];
        bool completed = true;
        for (int i = 0; i < log.Length; i++)
        {
            string name = npcsToTalkTo[i].displayedNameInSentence;
            string topic = npcsToTalkTo[i].displayedTopicInSentence;
            bool stepFinished = npcsTalkedTo[i];
            log[i] = ($"Porozmawiaj z {name} o {topic}.", stepFinished);
            if (!stepFinished)
            {
                completed = false;
            }
        }
        if (completed)
        {
            Complete();
        }
        ChangeData(state, log);
    }
    protected override void SetTaskState(string state)
    {
        npcsTalkedTo = JsonUtility.FromJson<bool[]>(state);
        UpdateState();
    }
}
