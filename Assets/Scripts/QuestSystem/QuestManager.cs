using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questsDict;
    private void Awake()
    {
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
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStart -= QuestStart;
        GameEventsManager.Instance.questEvents.OnTaskComplete -= TaskComplete;
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
}
