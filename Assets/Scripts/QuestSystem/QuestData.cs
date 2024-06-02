[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int currentTask;
    public TaskData[] tasksData;
    public long lastChanged;

    public QuestData(QuestState state, int currentTask, TaskData[] tasksData, long lastChanged)
    {
        this.state = state;
        this.currentTask = currentTask;
        this.tasksData = tasksData;
        this.lastChanged = lastChanged;
    }
}
