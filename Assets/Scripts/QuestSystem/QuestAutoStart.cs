using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to automaticaly start quests if requirements are met.
/// </summary>
public class QuestAutoStart : MonoBehaviour
{
    public QuestStaticSO[] quests;

    private HashSet<string> questsIds;

    /// <summary>
    /// Creates set of quests id's from given quests.
    /// </summary>
    private void Awake()
    {
        questsIds = new HashSet<string>();
        foreach (var quest in quests)
        {
            questsIds.Add(quest.Id);
        }
    }

    /// <summary>
    /// Checks if can start quest on quest state change.
    /// </summary>
    /// <param name="quest"></param>
    private void StartQuests(Quest quest)
    {
        StartCoroutine(StartQuestsAtTheEndOfFrame(quest));
    }

    /// <summary>
    /// Starts quest at the end of a frame if the quest is in the set.
    /// </summary>
    /// <param name="quest">Quest that changed state.</param>
    /// <returns>Ienumerator for a courutine.</returns>
    private IEnumerator StartQuestsAtTheEndOfFrame(Quest quest)
    {
        yield return new WaitForEndOfFrame();

        if (questsIds.Contains(quest.staticData.Id) && quest.state == QuestState.CanStart)
        {
            GameEventsManager.Instance.questEvents.QuestStart(quest.staticData.Id);
        }
    }

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStateChange += StartQuests;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnQuestStateChange -= StartQuests;
    }
}
