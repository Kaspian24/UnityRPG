using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages quests system.
/// </summary>
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private Dictionary<string, Quest> questsDict;
    public bool loading = true;

    /// <summary>
    /// Initializes singleton. Loads all quests with their state data if it exists.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        questsDict = new Dictionary<string, Quest>();
        QuestStaticSO[] questsToLoad = Resources.LoadAll<QuestStaticSO>("Quests");
        foreach (QuestStaticSO questStaticSO in questsToLoad)
        {
            questsDict.Add(questStaticSO.Id, LoadQuest(questStaticSO));
        }
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStart += QuestStart;
        GameEventsManager.Instance.questEvents.OnTaskComplete += TaskComplete;
        GameEventsManager.Instance.questEvents.OnTaskDataChange += TaskDataChange;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStart -= QuestStart;
        GameEventsManager.Instance.questEvents.OnTaskComplete -= TaskComplete;
        GameEventsManager.Instance.questEvents.OnTaskDataChange -= TaskDataChange;
    }

    /// <summary>
    /// Checks quests requirements and instantiates tasks for active quests. Triggers QuestStateChange event for every quest.
    /// </summary>
    private void Start()
    {
        CheckReqiurements();

        foreach (Quest quest in questsDict.Values)
        {
            if (quest.state == QuestState.Active)
            {
                _ = quest.InstantiateTask(transform);
            }
            GameEventsManager.Instance.questEvents.QuestStateChange(quest);
        }
        loading = false;
    }

    /// <summary>
    /// If all quests requirements are met sets them to active. If any antirequirements are met, sets the quest to unavailable.
    /// </summary>
    private void CheckReqiurements()
    {
        foreach (Quest quest in questsDict.Values)
        {
            if (quest.state == QuestState.Unavailable || quest.state == QuestState.Completed)
            {
                continue;
            }
            if (AntiRequirementsMet(quest))
            {
                QuestChangeState(quest.staticData.Id, QuestState.Unavailable);
            }
            else if (quest.state == QuestState.CannotStart && RequirementsMet(quest))
            {
                QuestChangeState(quest.staticData.Id, QuestState.CanStart);
            }
        }
    }

    /// <summary>
    /// Changes the quest state. Calls CheckRequirements. Triggers QuestStateChange event.
    /// </summary>
    /// <param name="id">Quest id.</param>
    /// <param name="questState">Quest state.</param>
    private void QuestChangeState(string id, QuestState questState)
    {
        Quest quest = questsDict[id];
        quest.state = questState;
        if (questState == QuestState.Unavailable)
        {
            QuestUnavailable(id);
        }
        CheckReqiurements();
        GameEventsManager.Instance.questEvents.QuestStateChange(quest);
    }

    /// <summary>
    /// Starts the quest. Triggers QuestChangeState event.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    private void QuestStart(string questId)
    {
        Quest quest = questsDict[questId];
        if (quest.InstantiateTask(transform))
        {
            QuestChangeState(quest.staticData.Id, QuestState.Active);
        }
    }

    /// <summary>
    /// Switches to next quest task or completes it if there are no more tasks.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    private void TaskComplete(string questId)
    {
        Quest quest = questsDict[questId];
        if (!quest.NextTask(transform))
        {
            GiveRewards(quest);
            QuestChangeState(quest.staticData.Id, QuestState.Completed);
        }
    }

    /// <summary>
    /// Triggers events ExpAdd, GoldAdd and ItemAdd based on quest rewards.
    /// </summary>
    /// <param name="quest">Completed quest.</param>
    private void GiveRewards(Quest quest)
    {
        GameEventsManager.Instance.playerEvents.ExpAdd(quest.staticData.experience);
        GameEventsManager.Instance.playerEvents.GoldAdd(quest.staticData.gold);
        foreach (RewardItemData rewardItemData in quest.staticData.items)
        {
            for (int i = 0; i < rewardItemData.amount; i++)
            {
                GameEventsManager.Instance.playerEvents.ItemAdd(rewardItemData.sceneItem.Item);
            }
        }
    }

    /// <summary>
    /// Updates task data.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    /// <param name="taskIndex">Task index.</param>
    /// <param name="taskData">Task data.</param>
    private void TaskDataChange(string questId, int taskIndex, TaskData taskData)
    {
        Quest quest = questsDict[questId];
        quest.UpdateTaskData(taskIndex, taskData);
    }

    /// <summary>
    /// Checks if all quest requirements are met.
    /// </summary>
    /// <param name="quest">Quest whose requirements are being checked.</param>
    /// <returns>True if all requirements are met, otherwise false.</returns>
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

    /// <summary>
    /// Checks if any quest antirequirements are met.
    /// </summary>
    /// <param name="quest">Quest whose antirequirements are being checked.</param>
    /// <returns>True if any requirements are met, otherwise false.</returns>
    private bool AntiRequirementsMet(Quest quest)
    {
        foreach (AntiRequirementData antiRequirement in quest.staticData.antiRequirements)
        {
            if (questsDict[antiRequirement.quest.Id].state == antiRequirement.state)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Gets list of quests sorted by last change date.
    /// </summary>
    /// <returns>Sorted quests list.</returns>
    public List<Quest> GetQuestsSortedByLastChanged()
    {
        List<Quest> questsList = new(questsDict.Values);
        questsList.Sort((a, b) => b.lastChanged.CompareTo(a.lastChanged));
        return questsList;
    }

    /// <summary>
    /// Getter for quest by id.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    /// <returns>Quest matching the id.</returns>
    public Quest GetQuestById(string questId)
    {
        Quest quest = questsDict[questId];
        return quest;
    }

    /// <summary>
    /// Method for saving quests data.
    /// </summary>
    /// <returns>Dictionary of quests ids and quests data.</returns>
    public Dictionary<string, QuestData> SaveQuests()
    {
        Dictionary<string, QuestData> questsData = new Dictionary<string, QuestData>();
        foreach (Quest quest in questsDict.Values)
        {
            questsData.Add(quest.staticData.Id, quest.GetQuestData());
        }
        return questsData;
    }

    /// <summary>
    /// Method for loading quest data from current save.
    /// </summary>
    /// <param name="questStaticSO">Quest static info.</param>
    /// <returns>Quest with new or loaded data.</returns>
    private Quest LoadQuest(QuestStaticSO questStaticSO)
    {
        if (SaveManager.Instance.currentSave.questsDataDict.TryGetValue(questStaticSO.Id, out QuestData questData))
        {
            return new Quest(questStaticSO, questData);
        }
        return new Quest(questStaticSO);
    }

    /// <summary>
    /// Destroys current task of a quest.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    private void QuestUnavailable(string questId)
    {
        Quest quest = questsDict[questId];
        quest.DestroyCurrentTask();
    }
}
