using System;

/// <summary>
/// Class with events related to quests.
/// </summary>
public class QuestEvents
{
    /// <summary>
    /// Triggered when quest gets started.
    /// </summary>
    public event Action<string> OnQuestStart;

    /// <summary>
    /// Invokes <see cref="OnQuestStart"/> event.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    public void QuestStart(string questId)
    {
        OnQuestStart?.Invoke(questId);
    }

    /// <summary>
    /// Triggered when quest changes state.
    /// </summary>
    public event Action<Quest> OnQuestStateChange;

    /// <summary>
    /// Invokes <see cref="OnQuestStateChange"/> event.
    /// </summary>
    /// <param name="quest">Quest that changed state.</param>
    public void QuestStateChange(Quest quest) // managed by QuestManager
    {
        OnQuestStateChange?.Invoke(quest);
    }

    /// <summary>
    /// Triggered when quest gets completed.
    /// </summary>
    public event Action<string> OnTaskComplete; // managed by QuestManager

    /// <summary>
    /// Invokes <see cref="OnTaskComplete"/> event.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    public void TaskComplete(string questId) // managed by QuestManager
    {
        OnTaskComplete?.Invoke(questId);
    }

    /// <summary>
    /// Triggered when task data gets changed.
    /// </summary>
    public event Action<string, int, TaskData> OnTaskDataChange;

    /// <summary>
    /// Invokes <see cref="OnTaskDataChange"/> event.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    /// <param name="taskIndex">Index of the task.</param>
    /// <param name="taskData">New task data.</param>
    public void TaskDataChange(string questId, int taskIndex, TaskData taskData)
    {
        OnTaskDataChange?.Invoke(questId, taskIndex, taskData);
    }

    /// <summary>
    /// Triggered when enemy dies.
    /// </summary>
    public event Action<string> OnEnemyDeath;

    /// <summary>
    /// Invokes <see cref="OnEnemyDeath"/> event.
    /// </summary>
    /// <param name="enemyType">Type of the enemy.</param>
    public void EnemyDeath(string enemyType)
    {
        OnEnemyDeath?.Invoke(enemyType);
    }

    /// <summary>
    /// Triggered when quest gets tracked.
    /// </summary>
    public event Action<string> OnQuestTrack;

    /// <summary>
    /// Invokes <see cref="OnQuestTrack"/> event.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    public void QuestTrack(string questId)
    {
        OnQuestTrack?.Invoke(questId);
    }

    /// <summary>
    /// Triggered when place gets visited.
    /// </summary>
    public event Action<string> OnPlaceVisited;

    /// <summary>
    /// Invokes <see cref="OnPlaceVisited"/> event.
    /// </summary>
    /// <param name="placeName">Name of the place.</param>
    public void PlaceVisited(string placeName)
    {
        OnPlaceVisited?.Invoke(placeName);
    }

    /// <summary>
    /// Triggered when place to visit gets enabled.
    /// </summary>
    public event Action<string> OnEnablePlaceToVisit;

    /// <summary>
    /// Invokes <see cref="OnEnablePlaceToVisit"/> event.
    /// </summary>
    /// <param name="placeName">Name of the place.</param>
    public void EnablePlaceToVisit(string placeName)
    {
        OnEnablePlaceToVisit?.Invoke(placeName);
    }
}
