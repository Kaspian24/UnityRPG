using UnityEngine;

public class Quest
{
    public QuestStaticSO staticData;
    public QuestState state;
    private int currentTask;

    public Quest(QuestStaticSO questStaticSO)
    {
        this.staticData = questStaticSO;
        state = QuestState.CannotStart;
        currentTask = 0;
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
            task.InitializeQuestStep(staticData.Id);
            return true;
        }
        return false;
    }
}
