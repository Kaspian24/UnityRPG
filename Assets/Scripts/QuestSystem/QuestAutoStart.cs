using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAutoStart : MonoBehaviour
{
    public QuestStaticSO[] quests;

    private HashSet<string> questsIds;

    private void Awake()
    {
        questsIds = new HashSet<string>();
        foreach (var quest in quests)
        {
            questsIds.Add(quest.Id);
        }
    }

    private void StartQuests(Quest quest)
    {
        StartCoroutine(StartQuestsAtTheEndOfFrame(quest));
    }

    private IEnumerator StartQuestsAtTheEndOfFrame(Quest quest)
    {
        yield return new WaitForEndOfFrame();

        if (questsIds.Contains(quest.staticData.Id) && quest.state == QuestState.CanStart)
        {
            GameEventsManager.Instance.questEvents.QuestStart(quest.staticData.Id);
        }
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStateChange += StartQuests;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStateChange -= StartQuests;
    }
}
