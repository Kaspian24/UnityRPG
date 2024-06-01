using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public string saveName;

    public Dictionary<string, QuestData> questsDataDict;

    public SaveData()
    {
        saveName = string.Empty;
        questsDataDict = new Dictionary<string, QuestData>();
    }

    public SaveData(string saveName, Dictionary<string, QuestData> questsDataDict)
    {
        this.saveName = saveName;
        this.questsDataDict = questsDataDict;
    }
}
