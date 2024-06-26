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

    public event Action<string> OnEnemyDeath;
    public void EnemyDeath(string enemyType)
    {
        OnEnemyDeath?.Invoke(enemyType);
    }

    public event Action<string> OnQuestTrack;
    public void QuestTrack(string questId)
    {
        OnQuestTrack?.Invoke(questId);
    }

    public event Action<string> OnPlaceVisited;
    public void PlaceVisited(string placeName)
    {
        OnPlaceVisited?.Invoke(placeName);
    }

    public event Action<string> OnEnablePlaceToVisit;
    public void EnablePlaceToVisit(string placeName)
    {
        OnEnablePlaceToVisit?.Invoke(placeName);
    }

    public event Action<string> OnQuestInavailable;
    public void QuestInavailable(string questId)
    {
        OnQuestInavailable?.Invoke(questId);
    }
}
