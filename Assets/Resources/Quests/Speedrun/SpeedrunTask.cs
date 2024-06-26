using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunTask : Task
{
    private void Start()
    {
        UpdateState();
    }
    private void EnemyDeath(string enemyType)
    {
        if(enemyType != "BlueDragon")
        {
            return;
        }
        GameEventsManager.Instance.dialogueEvents.EnableTopic("Szef", "Speedrun");
        Complete();
    }
    private void UpdateState()
    {
        string state = "";
        (string, bool)[] log = new (string, bool)[1];
        log[0] = ("Pokonaj smoka", true);
        ChangeData(state, log);
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnEnemyDeath += EnemyDeath;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnEnemyDeath -= EnemyDeath;
    }
    protected override void SetTaskState(string taskState)
    {
        UpdateState();
    }
}
