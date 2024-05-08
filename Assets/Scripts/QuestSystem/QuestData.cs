[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int currentTask;
    public TaskData[] taskDatas;
    public System.DateTime lastChanged;

    public QuestData(QuestState state, int currentTask, TaskData[] taskDatas, System.DateTime lastChanged)
    {
        this.state = state;
        this.currentTask = currentTask;
        this.taskDatas = taskDatas;
        this.lastChanged = lastChanged;
    }
}
