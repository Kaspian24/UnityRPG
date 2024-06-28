/// <summary>
/// Class containing non static task data.
/// </summary>
[System.Serializable]
public class TaskData
{
    public string state; // serialized task state
    public string description;
    public (string, bool)[] log;

    /// <summary>
    /// Constructor with arguments.
    /// </summary>
    /// <param name="state">Task state.</param>
    /// <param name="description">Task description.</param>
    /// <param name="log">Task log.</param>
    public TaskData(string state, string description, (string, bool)[] log)
    {
        this.description = description;
        this.state = state;
        this.log = log;
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public TaskData()
    {
        state = "";
        description = "";
        log = new (string, bool)[] { ("", false) };
    }
}
