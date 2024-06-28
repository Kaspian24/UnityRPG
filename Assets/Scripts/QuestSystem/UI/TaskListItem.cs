using TMPro;
using UnityEngine;

/// <summary>
/// Controlls single displayed task item.
/// </summary>
public class TaskListItem : MonoBehaviour
{
    public TMP_Text taskDescription;
    public GameObject taskLogPanel;
    public TMP_Text taskLogItemPrefab;

    /// <summary>
    /// Sets task log for displayed task.
    /// </summary>
    /// <param name="log"></param>
    public void SetLog((string, bool)[] log)
    {
        bool finished = true;
        foreach ((string, bool) logItem in log)
        {
            TMP_Text taskLogItem = Instantiate(taskLogItemPrefab, taskLogPanel.transform);
            taskLogItem.text = logItem.Item1;
            if (logItem.Item2)
            {
                taskLogItem.color = Color.yellow;
            }
            else
            {
                finished = false;
            }
        }
        if (finished)
        {
            taskDescription.color = Color.yellow;
        }
    }
}
