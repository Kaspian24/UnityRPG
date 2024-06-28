using UnityEngine;

/// <summary>
/// Quest class.
/// </summary>
public class Quest
{
    public QuestStaticSO staticData;
    public QuestState state;
    public int currentTask;
    public TaskData[] tasksData;
    public System.DateTime lastChanged;

    private Task instantiatedTask;

    /// <summary>
    /// Constructor for quest with no saved data.
    /// </summary>
    /// <param name="questStaticSO">Static quest info.</param>
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

    /// <summary>
    /// Constructor for quest with saved data.
    /// </summary>
    /// <param name="questStaticSO">Static quest info.</param>
    /// <param name="questData">Saved quest data.</param>
    public Quest(QuestStaticSO questStaticSO, QuestData questData)
    {
        this.staticData = questStaticSO;
        state = questData.state;
        currentTask = questData.currentTask;
        tasksData = questData.tasksData;
        lastChanged = System.DateTime.FromBinary(questData.lastChanged);
    }

    /// <summary>
    /// Instantiates next task.
    /// </summary>
    /// <param name="parrentTransform">Transform of the parent element.</param>
    /// <returns>True on success, false on failure.</returns>
    public bool NextTask(Transform parrentTransform)
    {
        if (1 + currentTask >= staticData.taskPrefabs.Length)
        {
            return false;
        }
        currentTask++;
        return InstantiateTask(parrentTransform);
    }

    /// <summary>
    /// Instantiates task.
    /// </summary>
    /// <param name="parrentTransform">Transform of the parent element.</param>
    /// <returns>True on success, false on failure.</returns>
    public bool InstantiateTask(Transform parrentTransform)
    {
        if (currentTask < staticData.taskPrefabs.Length)
        {
            GameObject taskPrefab = staticData.taskPrefabs[currentTask];
            Task task = Object.Instantiate<GameObject>(taskPrefab, parrentTransform).GetComponent<Task>();
            task.InitializeTask(staticData.Id, currentTask, tasksData[currentTask].state);
            instantiatedTask = task;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Updates task data.
    /// </summary>
    /// <param name="taskIndex">Task index.</param>
    /// <param name="taskData">Task data.</param>
    public void UpdateTaskData(int taskIndex, TaskData taskData)
    {
        tasksData[taskIndex] = taskData;
        lastChanged = System.DateTime.Now;
    }

    /// <summary>
    /// Getter for quest data. Used to save quest.
    /// </summary>
    /// <returns></returns>
    public QuestData GetQuestData()
    {
        return new QuestData(state, currentTask, tasksData, lastChanged.ToBinary());
    }

    /// <summary>
    /// Destroys currently instantiated task.
    /// </summary>
    public void DestroyCurrentTask()
    {
        if (instantiatedTask != null)
        {
            instantiatedTask.DestroyTask();
            instantiatedTask = null;
        }
    }
}
