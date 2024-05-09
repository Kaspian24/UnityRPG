using UnityEngine;

public class Quest
{
    public QuestStaticSO staticData;
    public QuestState state;
    public int currentTask;
    public TaskData[] tasksData;
    public System.DateTime lastChanged;

    public Quest(QuestStaticSO questStaticSO)
    {
        this.staticData = questStaticSO;
        state = QuestState.CannotStart;
        currentTask = 0;
        tasksData = new TaskData[staticData.taskPrefabs.Length];
        lastChanged = System.DateTime.MinValue;
        for (int i = 0; i < tasksData.Length; i++)
        {
            tasksData[i] = new TaskData();
        }
    }
    public Quest(QuestStaticSO questStaticSO, QuestData questData)
    {
        this.staticData = questStaticSO;
        state = questData.state;
        currentTask = questData.currentTask;
        tasksData = questData.tasksData;
        lastChanged = System.DateTime.MinValue;
    }
    public bool NextTask(Transform parrentTransform)
    {
        if (1 + currentTask >= staticData.taskPrefabs.Length)
        {
            return false;
        }
        currentTask++;
        return InstantiateTask(parrentTransform);
    }
    public bool InstantiateTask(Transform parrentTransform)
    {
        if (currentTask < staticData.taskPrefabs.Length)
        {
            GameObject taskPrefab = staticData.taskPrefabs[currentTask];
            Task task = Object.Instantiate<GameObject>(taskPrefab, parrentTransform).GetComponent<Task>();
            task.InitializeTask(staticData.Id, currentTask, tasksData[currentTask].state);
            return true;
        }
        return false;
    }
    public void UpdateTaskData(int taskIndex, TaskData taskData)
    {
        tasksData[taskIndex] = taskData;
        lastChanged = System.DateTime.Now;
    }
}
