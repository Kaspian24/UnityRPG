using UnityEngine;

public class Quest
{
    public QuestStaticSO staticData;
    public QuestState state;
    private int currentTask;
    public TaskData[] taskDatas;

    public Quest(QuestStaticSO questStaticSO)
    {
        this.staticData = questStaticSO;
        state = QuestState.CannotStart;
        currentTask = 0;
        taskDatas = new TaskData[staticData.taskPrefabs.Length];
        for (int i = 0; i < taskDatas.Length; i++)
        {
            taskDatas[i] = new TaskData();
        }
    }
    public Quest(QuestStaticSO questStaticSO, QuestData questData)
    {
        this.staticData = questStaticSO;
        state = questData.state;
        currentTask = questData.currentTask;
        taskDatas = questData.taskDatas;
    }
    public bool NextTask(Transform parrentTransform)
    {
        currentTask++;
        return InstantiateTask(parrentTransform);
    }
    public bool InstantiateTask(Transform parrentTransform)
    {
        if (currentTask < staticData.taskPrefabs.Length)
        {
            GameObject taskPrefab = staticData.taskPrefabs[currentTask];
            Task task = Object.Instantiate<GameObject>(taskPrefab, parrentTransform).GetComponent<Task>();
            task.InitializeTask(staticData.Id, currentTask, taskDatas[currentTask].state);
            return true;
        }
        return false;
    }
    public void UpdateTaskData(int taskIndex, TaskData taskData)
    {
        taskDatas[taskIndex] = taskData;
    }
}
