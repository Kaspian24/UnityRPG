using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// TaskKill data.
/// </summary>
[System.Serializable]
public class TaskKillData
{
    public string enemyType;
    public int enemyCount;
    [Header("Pokonaj 3 {displayedName}. (1/3)")]
    public string displayedNameInSentence;
}

/// <summary>
/// Kill enemy task type.
/// </summary>
public class TaskKill : Task
{
    public TaskKillData[] enemiesToKill;
    private int[] enemiesKilled;

    /// <summary>
    /// Increments number of enemies killed if the enemy type name matches with one from the array.
    /// </summary>
    /// <param name="enemyType">Name of the enemy type.</param>
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

    /// <summary>
    /// Initializes eniemiesKilled array, overwrites not specified displayed enemies names with default enemy type names.
    /// </summary>
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

    /// <summary>
    /// Calls update state on start.
    /// </summary>
    private void Start()
    {
        UpdateState();
    }

    /// <summary>
    /// Subscribes to enemy death event.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.OnEnemyDeath += EnemyKilled;
    }

    /// <summary>
    /// Unsubscribes from enemy death event.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.OnEnemyDeath -= EnemyKilled;
    }

    /// <summary>
    /// Updates task state.
    /// </summary>
    private void UpdateState()
    {
        string state = JsonConvert.SerializeObject(enemiesKilled);
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

    /// <inheritdoc/>
    protected override void SetTaskState(string state)
    {
        enemiesKilled = JsonConvert.DeserializeObject<int[]>(state);
        UpdateState();
    }
}
