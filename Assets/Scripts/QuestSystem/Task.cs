using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public string description;
    private bool isCompleted;
    private string questId;
    private int taskIndex;

    public void InitializeTask(string questId, int taskIndex, string taskState)
    {
        this.questId = questId;
        this.taskIndex = taskIndex;
        if (!string.IsNullOrEmpty(taskState))
        {
            SetTaskState(taskState);
        }
    }
    public void DestroyTask()
    {
        Destroy(gameObject);
    }
    protected void Complete()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            GameEventsManager.Instance.questEvents.TaskComplete(questId);
            Destroy(gameObject);
        }
    }
    protected void ChangeData(string state, (string, bool)[] log)
    {
        GameEventsManager.Instance.questEvents.TaskDataChange(questId, taskIndex, new TaskData(state, description, log));
    }
    protected abstract void SetTaskState(string taskState);
}
