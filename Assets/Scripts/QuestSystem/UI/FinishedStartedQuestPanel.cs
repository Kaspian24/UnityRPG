using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controlls finished/started quest panel.
/// </summary>
public class FinishedStartedQuestPanel : MonoBehaviour
{
    public TMP_Text questFinishedStartedMessage;
    public TMP_Text finishedStartedQuestTitle;

    private Queue<Quest> finishedStartedQuests = new Queue<Quest>();

    bool currentlyDisplayed;

    /// <summary>
    /// Adds quest to queue if it was completed or started.
    /// </summary>
    /// <param name="quest"></param>
    public void AddFinishedStartedQuest(Quest quest)
    {
        if (!(quest.state == QuestState.Completed || (quest.state == QuestState.Active && !quest.staticData.secret)))
        {
            if (!currentlyDisplayed)
            {
                gameObject.SetActive(false);
            }
            return;
        }
        finishedStartedQuests.Enqueue(quest);
        if (!currentlyDisplayed)
        {
            DisplayFinishedStartedMessage();
        }
    }

    /// <summary>
    /// Displays the panel with message.
    /// </summary>
    private void DisplayFinishedStartedMessage()
    {
        currentlyDisplayed = true;
        Quest quest = finishedStartedQuests.Dequeue();
        if (quest.state == QuestState.Active)
        {
            questFinishedStartedMessage.text = "Otrzymano nowe zadanie!";
        }
        else if (quest.staticData.secret)
        {
            questFinishedStartedMessage.text = "Ukoñczono ukryte zadanie!";
        }
        else
        {
            questFinishedStartedMessage.text = "Ukoñczono zadanie!";
        }
        finishedStartedQuestTitle.text = quest.staticData.title;
        StartCoroutine(FinishDisplaying());
    }

    /// <summary>
    /// Disables panel. Calls DisplayFinishedStartedMessage again if there are quests in the queue.
    /// </summary>
    /// <returns>Ienumerator for a courutine.</returns>
    private IEnumerator FinishDisplaying()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        questFinishedStartedMessage.text = "";
        finishedStartedQuestTitle.text = "";
        if (finishedStartedQuests.Count > 0)
        {
            DisplayFinishedStartedMessage();
        }
        else
        {
            currentlyDisplayed = false;
            gameObject.SetActive(false);
        }
    }
}
