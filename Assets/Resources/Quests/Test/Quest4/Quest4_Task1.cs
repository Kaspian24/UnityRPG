public class Quest4_Task1 : Task
{
    protected override void SetTaskData(string taskData)
    {

    }

    private void OnEnable()
    {
        Invoke("Complete", 3);
    }
}
