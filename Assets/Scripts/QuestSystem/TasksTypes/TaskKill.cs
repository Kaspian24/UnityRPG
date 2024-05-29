using UnityEngine;

[System.Serializable]
public class TaskKillData
{
    public string enemyType; // ToDo when enemies are ready change to instance of enemy and take the name from there
    public int enemyCount;
    [Header("Pokonaj 3 {displayedName}. (1/3)")]
    public string displayedNameInSentence;
}
public class TaskKill : Task
{
    public TaskKillData[] enemiesToKill;
    private int[] enemiesKilled;

    private void EnemyKilled(string enemyType)
    {
        for (int i = 0; i < enemiesToKill.Length; i++)
        {
            if (enemiesToKill[i].enemyType == enemyType)
            {
                enemiesKilled[i]++;
                UpdateState();
                return;
            }
        }
    }

    private void Awake()
    {
        enemiesKilled = new int[enemiesToKill.Length];
        foreach (TaskKillData enemy in enemiesToKill)
        {
            if (string.IsNullOrEmpty(enemy.displayedNameInSentence))
            {
                enemy.displayedNameInSentence = enemy.enemyType;
            }
        }
    }

    private void Start()
    {
        UpdateState();
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnEnemyDeath += EnemyKilled;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnEnemyDeath -= EnemyKilled;
    }
    private void UpdateState()
    {
        string state = JsonUtility.ToJson(enemiesKilled);
        (string, bool)[] log = new (string, bool)[enemiesToKill.Length];
        bool completed = true;
        for (int i = 0; i < log.Length; i++)
        {
            int needed = enemiesToKill[i].enemyCount;
            int killed = enemiesKilled[i];
            string name = enemiesToKill[i].displayedNameInSentence;
            bool stepFinished = killed >= needed;
            log[i] = ($"Pokonaj {needed} {name}. ({killed}/{needed})", stepFinished);
            if (!stepFinished)
            {
                completed = false;
            }
        }
        ChangeData(state, log);
        if (completed)
        {
            Complete();
        }
    }
    protected override void SetTaskState(string state)
    {
        enemiesKilled = JsonUtility.FromJson<int[]>(state);
        UpdateState();
    }
}
