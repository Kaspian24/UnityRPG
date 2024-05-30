using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishedStartedQuestPanel : MonoBehaviour
{
    public TMP_Text questFinishedStartedMessage;
    public TMP_Text finishedStartedQuestTitle;

    private Queue<Quest> finishedStartedQuests = new Queue<Quest>();

    bool currentlyDisplayed;

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
