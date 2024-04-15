using UnityEngine;

public abstract class Task : MonoBehaviour
{
    private bool isCompleted;
    private string questId;

    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
    }
    protected void Complete()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            GameEventsManager.Instance.questEvents.TaskComplete(questId);
            Destroy(gameObject);
        }
    }

}
