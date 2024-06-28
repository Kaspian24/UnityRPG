using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing save data.
/// </summary>
[System.Serializable]
public class SaveData
{
    public string saveName;

    public Dictionary<string, QuestData> questsDataDict;

    public string playerControllerPosition;

    public string playerControllerRotation;

    public string currentlyTrackedQuest;

    public List<KeyValuePair<string, int>> items;

    public List<KeyValuePair<string, string>> equippedItems;

    public Dictionary<string, int> stats;

    public HashSet<(string, string)> enabledTopics;

    public long saveDate;

    [JsonIgnore]
    public bool enabled;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public SaveData()
    {
        saveName = string.Empty;
        questsDataDict = new Dictionary<string, QuestData>();
        playerControllerPosition = JsonUtility.ToJson(Vector3.zero);
        playerControllerRotation = JsonUtility.ToJson(Quaternion.identity);
        currentlyTrackedQuest = string.Empty;
        items = new List<KeyValuePair<string, int>>();
        equippedItems = new List<KeyValuePair<string, string>>();
        stats = new Dictionary<string, int>();
        enabledTopics = new HashSet<(string, string)>();
        saveDate = 0;
        enabled = false;
    }

    /// <summary>
    /// Constructor with arguments.
    /// </summary>
    /// <param name="saveName">Name of the save file.</param>
    /// <param name="questsDataDict">Quests data.</param>
    /// <param name="playerControllerPosition">Player position.</param>
    /// <param name="playerControllerRotation">Player rotation</param>
    /// <param name="currentlyTrackedQuest">Tracked quest.</param>
    /// <param name="items">Player items.</param>
    /// <param name="equippedItems">Player equipped items.</param>
    /// <param name="stats">Player stats.</param>
    /// <param name="enabledTopics">Enabled dialogue topics.</param>
    /// <param name="saveDate">Date of the save in binary.</param>
    public SaveData(string saveName, Dictionary<string, QuestData> questsDataDict, Vector3 playerControllerPosition, Quaternion playerControllerRotation, string currentlyTrackedQuest, List<KeyValuePair<string, int>> items, List<KeyValuePair<string, string>> equippedItems, Dictionary<string, int> stats, HashSet<(string, string)> enabledTopics, long saveDate)
    {
        this.saveName = saveName;
        this.questsDataDict = questsDataDict;
        this.playerControllerPosition = JsonUtility.ToJson(playerControllerPosition);
        this.playerControllerRotation = JsonUtility.ToJson(playerControllerRotation);
        this.currentlyTrackedQuest = currentlyTrackedQuest;
        this.items = items;
        this.equippedItems = equippedItems;
        this.stats = stats;
        this.enabledTopics = enabledTopics;
        this.saveDate = saveDate;
        enabled = true;
    }
}
