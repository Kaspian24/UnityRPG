using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AntiRequirementsData
{
    public QuestStaticSO quest;
    public QuestState state;
}

[Serializable]
public class InavailableRequirementsData
{
    public QuestStaticSO quest;
    public AntiRequirementsData[] antiRequirements;
}

public class QuestAutoStart : MonoBehaviour
{
    public QuestStaticSO[] quests;

    public InavailableRequirementsData[] inavailableRequirements;

    private HashSet<string> questsIds;

    private HashSet<string> inavailableRequirementsQuestIds;

    private void Awake()
    {
        questsIds = new HashSet<string>();
        inavailableRequirementsQuestIds = new HashSet<string>();
        foreach (var quest in quests)
        {
            questsIds.Add(quest.Id);
        }

        foreach (InavailableRequirementsData inavailableRequirement in inavailableRequirements)
        {
            foreach (AntiRequirementsData antiRequirement in inavailableRequirement.antiRequirements)
            {
                inavailableRequirementsQuestIds.Add(antiRequirement.quest.Id);
            }
        }
    }

    private void ProcessQuests(Quest quest)
    {
        StartCoroutine(ProcessQuestsAtTheEndOfFrame(quest));
    }

    private IEnumerator ProcessQuestsAtTheEndOfFrame(Quest quest)
    {
        yield return new WaitForEndOfFrame();

        if (questsIds.Contains(quest.staticData.Id) && quest.state == QuestState.CanStart)
        {
            GameEventsManager.Instance.questEvents.QuestStart(quest.staticData.Id);
        }
        else if(inavailableRequirementsQuestIds.Contains(quest.staticData.Id))
        {
            QuestContainedInInavailableRequirements(quest);
        }
    }

    private void QuestContainedInInavailableRequirements(Quest quest)
    {
        foreach(InavailableRequirementsData inavailableRequirement in inavailableRequirements)
        {
            Quest currentlyCheckedQuest = QuestManager.Instance.GetQuestById(inavailableRequirement.quest.Id);
            if(currentlyCheckedQuest.state == QuestState.Completed || currentlyCheckedQuest.state == QuestState.Inavailable)
            {
                break;
            }
            foreach (AntiRequirementsData antiRequirement in inavailableRequirement.antiRequirements)
            {
                if(antiRequirement.quest.Id == quest.staticData.Id && antiRequirement.state == quest.state)
                {
                    GameEventsManager.Instance.questEvents.QuestInavailable(currentlyCheckedQuest.staticData.Id);
                    break;
                }
            }
        }
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStateChange += ProcessQuests;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStateChange -= ProcessQuests;
    }
}
