using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishedQuestPanel : MonoBehaviour
{
    public TMP_Text questFinishedMessage;
    public TMP_Text finishedQuestTitle;

    private Queue<Quest> finishedQuests = new Queue<Quest>();

    bool currentlyDisplayed;

    public void AddFinishedQuest(Quest quest)
    {
        if(quest.state != QuestState.Completed)
        {
            gameObject.SetActive(false);
            return;
        }
        finishedQuests.Enqueue(quest);
        if(!currentlyDisplayed)
        {
            DisplayFinishedMessage();
        }
    }

    private void DisplayFinishedMessage()
    {
        currentlyDisplayed = true;
        Quest quest = finishedQuests.Dequeue();
        if(quest.staticData.secret)
        {
            questFinishedMessage.text = "Ukoñczono ukryte zadanie!";
        }
        else
        {
            questFinishedMessage.text = "Ukoñczono zadanie!";
        }
        finishedQuestTitle.text = quest.staticData.title;
        StartCoroutine(FinishDisplaying());
    }

    private IEnumerator FinishDisplaying()
    {
        yield return new WaitForSeconds(3.0f);
        gameObject.SetActive(false);
        questFinishedMessage.text = "";
        finishedQuestTitle.text = "";
        currentlyDisplayed = false;
        if(finishedQuests.Count > 0)
        {
            DisplayFinishedMessage();
        }
    }
}
