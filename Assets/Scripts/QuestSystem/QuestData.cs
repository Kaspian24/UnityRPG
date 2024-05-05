[System.Serializable]
public class QuestData
{
    public QuestState state;
    public int currentTask;
    public TaskData[] taskDatas;

    public QuestData(QuestState state, int currentTask, TaskData[] taskDatas)
    {
        this.state = state;
        this.currentTask = currentTask;
        this.taskDatas = taskDatas;
    }
}
