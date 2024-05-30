using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private Dictionary<string, Quest> questsDict;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        questsDict = new Dictionary<string, Quest>();
        QuestStaticSO[] questsToLoad = Resources.LoadAll<QuestStaticSO>("Quests"); // Load SO's from location
        foreach (QuestStaticSO questStaticSO in questsToLoad)
        {
            questsDict.Add(questStaticSO.Id, new Quest(questStaticSO));
        }
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStart += QuestStart;
        GameEventsManager.Instance.questEvents.OnTaskComplete += TaskComplete;
        GameEventsManager.Instance.questEvents.OnTaskDataChange += TaskDataChange;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStart -= QuestStart;
        GameEventsManager.Instance.questEvents.OnTaskComplete -= TaskComplete;
        GameEventsManager.Instance.questEvents.OnTaskDataChange -= TaskDataChange;
    }
    private void Start()
    {
        // check quests requirements on start
        CheckReqiurements();

        foreach (Quest quest in questsDict.Values)
        {
            // inform about all quests states on start
            GameEventsManager.Instance.questEvents.QuestStateChange(quest);
        }
    }
    private void CheckReqiurements()
    {
        foreach (Quest quest in questsDict.Values)
        {
            if (quest.state == QuestState.CannotStart && RequirementsMet(quest))
            {
                QuestChangeState(quest.staticData.Id, QuestState.CanStart);
            }
        }
    }
    private void QuestChangeState(string id, QuestState questState)
    {
        Quest quest = questsDict[id];
        quest.state = questState;
        GameEventsManager.Instance.questEvents.QuestStateChange(quest);
    }
    private void QuestStart(string questId)
    {
        Quest quest = questsDict[questId];
        if (quest.InstantiateTask(transform))
        {
            QuestChangeState(quest.staticData.Id, QuestState.Active);
        }
    }
    private void TaskComplete(string questId)
    {
        Quest quest = questsDict[questId];
        if (!quest.NextTask(transform))
        {
            GiveRewards(quest);
            QuestChangeState(quest.staticData.Id, QuestState.Completed);
            CheckReqiurements();
        }
    }
    private void GiveRewards(Quest quest)
    {
        // ToDo when there's player stats and inventory
        Debug.Log("Given Reward");
    }
    private void TaskDataChange(string questId, int taskIndex, TaskData taskData)
    {
        Quest quest = questsDict[questId];
        quest.UpdateTaskData(taskIndex, taskData);
    }
    private bool RequirementsMet(Quest quest)
    {
        foreach (QuestStaticSO questStaticSO in quest.staticData.questsRequired)
        {
            if (questsDict[questStaticSO.Id].state != QuestState.Completed)
            {
                return false;
            }
        }
        return true;
    }

    public List<Quest> GetQuestsSortedByLastChanged()
    {
        List<Quest> questsList = new(questsDict.Values);
        questsList.Sort((a, b) => b.lastChanged.CompareTo(a.lastChanged));
        return questsList;
    }

    public Quest GetQuestById(string questId)
    {
        Quest quest = questsDict[questId];
        return quest;
    }
}
