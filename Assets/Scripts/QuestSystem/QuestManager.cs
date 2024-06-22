using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private Dictionary<string, Quest> questsDict;
    public bool loading = true;
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
            questsDict.Add(questStaticSO.Id, LoadQuest(questStaticSO));
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
            if(quest.state == QuestState.Active)
            {
                _ = quest.InstantiateTask(transform);
            }
            // inform about all quests states on start
            GameEventsManager.Instance.questEvents.QuestStateChange(quest);
        }
        loading = false;
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
        GameEventsManager.Instance.playerEvents.ExpAdd(quest.staticData.experience);
        GameEventsManager.Instance.playerEvents.GoldAdd(quest.staticData.gold);
        foreach (RewardItemData item in quest.staticData.items)
        {
            GameEventsManager.Instance.playerEvents.ItemAdd(item.amount, item.name);
        }
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

    public Dictionary<string, QuestData> SaveQuests()
    {
        Dictionary<string, QuestData> questsData = new Dictionary<string, QuestData>();
        foreach (Quest quest in questsDict.Values)
        {
            questsData.Add(quest.staticData.Id, quest.GetQuestData());
        }
        return questsData;
    }

    private Quest LoadQuest(QuestStaticSO questStaticSO)
    {
        if (SaveManager.Instance.currentSave.questsDataDict.TryGetValue(questStaticSO.Id, out QuestData questData))
        {
            return new Quest(questStaticSO, questData);
        }
        return new Quest(questStaticSO);
    }
}
