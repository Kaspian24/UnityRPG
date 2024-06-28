/// <summary>
/// Class containing non static quest data.
/// </summary>
[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int currentTask;
    public TaskData[] tasksData;
    public long lastChanged;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="state">State of the quest.</param>
    /// <param name="currentTask">Current task.</param>
    /// <param name="tasksData">Tasks data.</param>
    /// <param name="lastChanged">Date of the last quest progress in binary.</param>
    public QuestData(QuestState state, int currentTask, TaskData[] tasksData, long lastChanged)
    {
        this.state = state;
        this.currentTask = currentTask;
        this.tasksData = tasksData;
        this.lastChanged = lastChanged;
    }
}
