/// <summary>
/// Finish game task type.
/// </summary>
public class FinishGame : Task
{
    /// <summary>
    /// Calls update state on start.
    /// </summary>
    private void Start()
    {
        UpdateState();
    }

    /// <summary>
    /// Updates task state.
    /// </summary>
    private void UpdateState()
    {
        string state = "";
        (string, bool)[] log = new (string, bool)[1];
        log[0] = ("Ukoñcz grê", true);
        ChangeData(state, log);
        GameEventsManager.Instance.gameModeEvents.GameFinishedMessageOnOff(true);
        Complete();
    }

    /// <summary>
    /// Calls update state.
    /// </summary>
    /// <param name="taskState">Task state, not used in this task type.</param>
    protected override void SetTaskState(string taskState)
    {
        UpdateState();
    }
}
