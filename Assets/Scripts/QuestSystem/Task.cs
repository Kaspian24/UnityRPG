using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    private bool isCompleted;
    private string questId;
    private int taskIndex;
    private List<Tuple<string, bool>> log;

    public void InitializeTask(string questId, int taskIndex, string taskData)
    {
        this.questId = questId;
        this.taskIndex = taskIndex;
        if (!string.IsNullOrEmpty(taskData))
        {
            SetTaskData(taskData);
        }
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
    protected void ChangeData(string state)
    {
        GameEventsManager.Instance.questEvents.TaskDataChange(questId, taskIndex, new TaskData(state, log));
    }
    protected abstract void SetTaskData(string taskData);
}
