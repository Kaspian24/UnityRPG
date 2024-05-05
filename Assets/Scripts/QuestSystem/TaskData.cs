using System;
using System.Collections.Generic;

[System.Serializable]
public class TaskData
{
    public string state; // serialized task state
    public List<Tuple<string, bool>> log;

    public TaskData(string state, List<Tuple<string, bool>> log)
    {
        this.state = state;
        this.log = log;
    }
    public TaskData()
    {
        state = "";
        log = new List<Tuple<string, bool>>();
    }
}
