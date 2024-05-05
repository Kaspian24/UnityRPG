using System;

public class QuestEvents
{
    public event Action<string> OnQuestStart; // managed by QuestManager
    public void QuestStart(string questId) // call this to start quest
    {
        OnQuestStart?.Invoke(questId);
    }

    public event Action<Quest> OnQuestStateChange; // subscribe to this to react on quest state change
    public void QuestStateChange(Quest quest) // managed by QuestManager
    {
        OnQuestStateChange?.Invoke(quest);
    }

    public event Action<string> OnTaskComplete; // managed by QuestManager
    public void TaskComplete(string questId) // managed by QuestManager
    {
        OnTaskComplete?.Invoke(questId);
    }

    public event Action<string, int, TaskData> OnTaskDataChange;
    public void TaskDataChange(string questId, int taskIndex, TaskData taskData)
    {
        OnTaskDataChange?.Invoke(questId, taskIndex, taskData);
    }
}
