using UnityEngine;

/// <summary>
/// Task of a quest.
/// </summary>
public abstract class Task : MonoBehaviour
{
    public string description;
    private bool isCompleted;
    private string questId;
    private int taskIndex;

    /// <summary>
    /// Initializes task with beginning state.
    /// </summary>
    /// <param name="questId">Id of the quest.</param>
    /// <param name="taskIndex">Task index.</param>
    /// <param name="taskState">Task state.</param>
    public void InitializeTask(string questId, int taskIndex, string taskState)
    {
        this.questId = questId;
        this.taskIndex = taskIndex;
        if (!string.IsNullOrEmpty(taskState))
        {
            SetTaskState(taskState);
        }
    }

    /// <summary>
    /// Destroys the task game object.
    /// </summary>
    public void DestroyTask()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Triggers task complete event, destroys the task game object.
    /// </summary>
    protected void Complete()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            GameEventsManager.Instance.questEvents.TaskComplete(questId);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Triggers task data change event.
    /// </summary>
    /// <param name="state">Task state.</param>
    /// <param name="log">Task log.</param>
    protected void ChangeData(string state, (string, bool)[] log)
    {
        GameEventsManager.Instance.questEvents.TaskDataChange(questId, taskIndex, new TaskData(state, description, log));
    }

    /// <summary>
    /// Sets task state. Each task must deserialize the state variables from json.
    /// </summary>
    /// <param name="taskState">Task state.</param>
    protected abstract void SetTaskState(string taskState);
}
