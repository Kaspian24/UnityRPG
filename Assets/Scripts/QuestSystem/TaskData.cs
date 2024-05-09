[System.Serializable]
public class TaskData
{
    public string state; // serialized task state
    public string description;
    public (string, bool)[] log;

    public TaskData(string state, string description, (string, bool)[] log)
    {
        this.description = description;
        this.state = state;
        this.log = log;
    }
    public TaskData()
    {
        state = "";
        description = "";
        log = new (string, bool)[] { ("", false) };
    }
}
