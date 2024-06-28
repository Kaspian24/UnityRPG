using UnityEngine;

/// <summary>
/// Reward item data.
/// </summary>
[System.Serializable]
public class RewardItemData
{
    public int amount;
    public SceneItem sceneItem;
}

/// <summary>
/// Anti requirements data.
/// </summary>
[System.Serializable]
public class AntiRequirementData
{
    public QuestStaticSO quest;
    public QuestState state;
}

/// <summary>
/// Static quest info Scriptable Object.
/// </summary>
[CreateAssetMenu(fileName = "QuestStaticSO", menuName = "Quests/QuestStaticSO", order = 1)]
public class QuestStaticSO : ScriptableObject
{
    [field: SerializeField]
    public string Id { get; private set; }

    [Header("Info")]
    public string title;
    [TextArea]
    public string description;
    public bool secret;

    [Header("Requirements")]
    public QuestStaticSO[] questsRequired;
    public AntiRequirementData[] antiRequirements;

    [Header("Tasks")]
    public GameObject[] taskPrefabs;

    [Header("Rewards")]
    public int gold;
    public int experience;
    public RewardItemData[] items;

    /// <summary>
    /// Enforces that the id and file name is the same as Id.
    /// </summary>
    private void OnValidate()
    {
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
