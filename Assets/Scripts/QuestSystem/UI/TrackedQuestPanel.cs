using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Controlls tracked quest panel.
/// </summary>
public class TrackedQuestPanel : MonoBehaviour
{
    public TMP_Text questTitle;
    public TMP_Text taskDescription;
    public GameObject taskListPanel;
    public TMP_Text taskListItemPrefab;
    private List<GameObject> instantiatedTaskListItems = new List<GameObject>();

    private string questId = "";

    /// <summary>
    /// Updates tracked quest info. If there's no currently tracked quest or it has been finished - searches for last changed active (not secret) quest.
    /// </summary>
    public void UpdateTracked()
    {
        ClearInstantiated(instantiatedTaskListItems);
        Quest quest = null;
        if (!string.IsNullOrEmpty(questId))
        {
            quest = QuestManager.Instance.GetQuestById(questId);
        }
        if (quest == null || quest.state != QuestState.Active || quest.staticData.secret)
        {
            questId = "";
            quest = null;
            List<Quest> quests = QuestManager.Instance.GetQuestsSortedByLastChanged();
            foreach (Quest questInList in quests)
            {
                if (questInList.state == QuestState.Active && !questInList.staticData.secret)
                {
                    quest = questInList;
                    questId = quest.staticData.Id;
                    break;
                }
            }
            if (quest == null)
            {
                gameObject.SetActive(false);
                return;
            }
        }
        questTitle.text = quest.staticData.title;
        int currentTask = quest.currentTask;
        taskDescription.text = quest.tasksData[currentTask].description;
        foreach ((string, bool) logItem in quest.tasksData[currentTask].log)
        {
            if (!logItem.Item2)
            {
                TMP_Text taskLogItem = Instantiate(taskListItemPrefab, taskListPanel.transform);
                taskLogItem.text = logItem.Item1;
                instantiatedTaskListItems.Add(taskLogItem.gameObject);
            }
        }
        if (GameModeManager.Instance.currentGameMode == GameMode.Dialogue)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Sets tracked quest. Calls UpdateTracked.
    /// </summary>
    /// <param name="questId"></param>
    public void TrackQuest(string questId)
    {
        this.questId = questId;
        UpdateTracked();
    }

    /// <summary>
    /// Getter for currently tracked quest.
    /// </summary>
    /// <returns>Id of the currently tracked quest.</returns>
    public string GetCurrentlyTrackedQuest()
    {
        return this.questId;
    }

    /// <summary>
    /// Clears list of instatniated game objects.
    /// </summary>
    /// <param name="gameObjects">Game object list to clear.</param>
    private void ClearInstantiated(List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        gameObjects.Clear();
    }
}
