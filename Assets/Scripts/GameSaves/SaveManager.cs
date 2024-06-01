using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public GameObject playerController;

    public SaveData currentSave;

    private string savesPath = "Assets/Resources/Saves";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        currentSave = new SaveData();
    }

    public void Save(string name, bool canOverride)
    {
        string path = Path.Combine(savesPath, name);
        if (File.Exists(path) && !canOverride)
        {
            Debug.Log("Plik zapisu ju¿ istnieje.");
            return;
        }
        Directory.CreateDirectory(path);
        Dictionary<string, QuestData> questsData = QuestManager.Instance.SaveQuests();
        currentSave = new SaveData(name, questsData);
        string currentSaveJson = JsonUtility.ToJson(currentSave);
        using StreamWriter sw = new StreamWriter(path);
        sw.Write(currentSaveJson);
    }

    public void Load(string name)
    {
        string path = Path.Combine(savesPath, name);
        if (!File.Exists(path))
        {
            Debug.Log("Plik zapisu nie istnieje.");
            return;
        }
        currentSave = JsonUtility.FromJson<SaveData>(path);
    }

    public Dictionary<string, SaveData> GetSaveFiles()
    {
        Dictionary<string, SaveData> savesDict = new Dictionary<string, SaveData>();

        return savesDict;
    }
}
