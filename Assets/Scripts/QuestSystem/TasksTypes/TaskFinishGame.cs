public class FinishGame : Task
{
    private void Start()
    {
        UpdateState();
    }
    private void UpdateState()
    {
        string state = "";
        (string, bool)[] log = new (string, bool)[1];
        log[0] = ("Ukoñcz grê", true);
        ChangeData(state, log);
        GameEventsManager.Instance.gameModeEvents.GameFinishedMessageOnOff(true);
        Complete();
    }
    protected override void SetTaskState(string taskState)
    {
        UpdateState();
    }
}
