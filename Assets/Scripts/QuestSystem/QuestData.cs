[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int currentTask;
    public TaskData[] tasksData;
    public System.DateTime lastChanged;

    public QuestData(QuestState state, int currentTask, TaskData[] tasksData, System.DateTime lastChanged)
    {
        this.state = state;
        this.currentTask = currentTask;
        this.tasksData = tasksData;
        this.lastChanged = lastChanged;
    }
}
