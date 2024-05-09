using UnityEngine;

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

    [Header("Tasks")]
    public GameObject[] taskPrefabs;

    [Header("Rewards")]
    public int gold;
    public int experience;
    //public List<Tuple<int, Item>> items;

    private void OnValidate() // Enforces that the id and file name is the same as Id
    {
#if UNITY_EDITOR
        Id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
