using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlls quest menu panel.
/// </summary>
public class QuestMenuPanel : MonoBehaviour
{
    public GameObject questListItemPrefab;
    public GameObject questListPanel;
    public TMP_Text questInfoPanelTitle;
    public TMP_Text questInfoPanelDescription;
    public Button questInfoPanelTrackButton;
    public GameObject taskListPanel;
    public GameObject taskListItemPrefab;

    private List<GameObject> instantiatedQuestListItems = new List<GameObject>();
    private List<GameObject> instantiatedTaskListItems = new List<GameObject>();

    /// <summary>
    /// Displays all active (not secret) or completed quests sorted by last change. Selects the first one for description panel.
    /// </summary>
    private void OnEnable()
    {
        List<Quest> questList = QuestManager.Instance.GetQuestsSortedByLastChanged();
        foreach (Quest quest in questList)
        {
            if (quest.state == QuestState.Unavailable)
            {
                continue;
            }
            if (quest.state < QuestState.Active)
            {
                continue;
            }
            if (quest.staticData.secret == true && quest.state < QuestState.Completed)
            {
                continue;
            }
            GameObject questListItem = Instantiate(questListItemPrefab, questListPanel.transform);
            questListItem.GetComponentInChildren<TMP_Text>().text = quest.staticData.title;
            if (quest.state == QuestState.Completed)
            {
                questListItem.GetComponentInChildren<TMP_Text>().color = Color.yellow;
            }
            questListItem.GetComponent<Button>().onClick.AddListener(() => SelectQuest(quest.staticData.Id));
            instantiatedQuestListItems.Add(questListItem);
        }
        if (instantiatedQuestListItems.Count > 0)
        {
            instantiatedQuestListItems[0].GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            questInfoPanelTrackButton.gameObject.SetActive(false);
            questInfoPanelTitle.text = "";
            questInfoPanelDescription.text = "";
        }
    }

    /// <summary>
    /// Displays quest description,
    /// </summary>
    /// <param name="questId">Id of the quest whose description will be displayed.</param>
    private void SelectQuest(string questId)
    {
        ClearInstantiated(instantiatedTaskListItems);
        Quest quest = QuestManager.Instance.GetQuestById(questId);
        if (quest.state == QuestState.Active)
        {
            questInfoPanelTrackButton.gameObject.SetActive(true);
            questInfoPanelTrackButton.onClick.RemoveAllListeners();
            questInfoPanelTrackButton.onClick.AddListener(() => GameEventsManager.Instance.questEvents.QuestTrack(quest.staticData.Id));
        }
        questInfoPanelTitle.text = quest.staticData.title;
        questInfoPanelDescription.text = quest.staticData.description;
        for (int i = 0; i <= quest.currentTask; i++)
        {
            TaskData taskData = quest.tasksData[i];
            GameObject taskListItem = Instantiate(taskListItemPrefab, taskListPanel.transform);
            taskListItem.GetComponent<TaskListItem>().taskDescription.text = taskData.description;
            taskListItem.GetComponent<TaskListItem>().SetLog(taskData.log);
            instantiatedTaskListItems.Add(taskListItem);
        }
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

    /// <summary>
    /// Clears instantiated quests and tasks descriptions.
    /// </summary>
    private void OnDisable()
    {
        ClearInstantiated(instantiatedQuestListItems);
        ClearInstantiated(instantiatedTaskListItems);
    }
}
