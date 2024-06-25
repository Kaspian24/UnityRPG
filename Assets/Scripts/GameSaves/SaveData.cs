using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    public long saveDate;

    [JsonIgnore]
    public bool enabled;

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
        saveDate = 0;
        enabled = false;
    }

    public SaveData(string saveName, Dictionary<string, QuestData> questsDataDict, Vector3 playerControllerPosition, Quaternion playerControllerRotation, string currentlyTrackedQuest, List<KeyValuePair<string, int>> items, List<KeyValuePair<string, string>> equippedItems, Dictionary<string, int> stats, long saveDate)
    {
        this.saveName = saveName;
        this.questsDataDict = questsDataDict;
        this.playerControllerPosition = JsonUtility.ToJson(playerControllerPosition);
        this.playerControllerRotation = JsonUtility.ToJson(playerControllerRotation);
        this.currentlyTrackedQuest = currentlyTrackedQuest;
        this.items = items;
        this.equippedItems = equippedItems;
        this.stats = stats;
        this.saveDate = saveDate;
        enabled = true;
    }
}
