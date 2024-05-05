public class Quest4_Task3 : Task
{
    protected override void SetTaskData(string taskData)
    {

    }

    private void OnEnable()
    {
        Invoke("Complete", 3);
    }
}
