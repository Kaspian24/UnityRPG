using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// TaskTalk data.
/// </summary>
[System.Serializable]
public class TaskTalkData
{
    public string npcName;
    public string conversationTopic;
    [Header("Porozmawiaj z {displayedName} o {displayedTopic}.")]
    public string displayedNameInSentence;
    public string displayedTopicInSentence;
}

/// <summary>
/// Talk to npc task type.
/// </summary>
public class TaskTalk : Task
{
    public TaskTalkData[] npcsToTalkTo;
    private bool[] npcsTalkedTo;

    /// <summary>
    /// Toggles npc talked to if the npc and topic name matches with one from the array.
    /// </summary>
    /// <param name="npcName">Name of the npc</param>
    /// <param name="conversationTopic">Conversation topic</param>
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

    /// <summary>
    /// Initializes npcsTalkedTo array, overwrites not specified displayed npc and topic names with defaults.
    /// </summary>
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

    /// <summary>
    /// Enables not yet talked topics. Calls update state on start.
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < npcsToTalkTo.Length; i++)
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

    /// <summary>
    /// Subscribes to topic talked about event.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.OnTopicTalkedAbout += TopicTalkedAbout;
    }

    /// <summary>
    /// Unsubscribes from topic talked about event.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.OnTopicTalkedAbout -= TopicTalkedAbout;
    }

    /// <summary>
    /// Updates task state.
    /// </summary>
    private void UpdateState()
    {
        string state = JsonConvert.SerializeObject(npcsTalkedTo);
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
        ChangeData(state, log);
        if (completed)
        {
            Complete();
        }
    }

    /// <inheritdoc/>
    protected override void SetTaskState(string state)
    {
        npcsTalkedTo = JsonConvert.DeserializeObject<bool[]>(state);
        UpdateState();
    }
}
