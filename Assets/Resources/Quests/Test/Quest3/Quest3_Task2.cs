public class Quest4_Task2 : Task
{
    protected override void SetTaskData(string taskData)
    {

    }

    private void OnEnable()
    {
        Invoke("Complete", 3);
    }
}
