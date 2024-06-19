using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string saveName;

    public Dictionary<string, QuestData> questsDataDict;

    public string playerControllerPosition;

    public string playerControllerRotation;

    public string currentlyTrackedQuest;

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
        saveDate = 0;
        enabled = false;
    }

    public SaveData(string saveName, Dictionary<string, QuestData> questsDataDict, Vector3 playerControllerPosition, Quaternion playerControllerRotation, string currentlyTrackedQuest, long saveDate)
    {
        this.saveName = saveName;
        this.questsDataDict = questsDataDict;
        this.playerControllerPosition = JsonUtility.ToJson(playerControllerPosition);
        this.playerControllerRotation = JsonUtility.ToJson(playerControllerRotation);
        this.currentlyTrackedQuest = currentlyTrackedQuest;
        this.saveDate = saveDate;
        enabled = true;
    }
}
