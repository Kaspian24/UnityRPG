using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private GameObject playerController;

    public SaveData currentSave = new SaveData();

    private string savesPath = "Assets/Resources/Saves";

    private string sceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        sceneName = SceneManager.GetActiveScene().name;
    }

    public void Save(string name, bool canOverride)
    {
        Directory.CreateDirectory(savesPath);
        string path = Path.Combine(savesPath, name + ".json");
        if (File.Exists(path) && !canOverride)
        {
            Debug.Log("Plik zapisu ju� istnieje.");
            return;
        }
        Dictionary<string, QuestData> questsData = QuestManager.Instance.SaveQuests();
        string currentlyTrackedQuest = QuestMenuManager.Instance.GetCurrentlyTrackedQuest();
        currentSave = new SaveData(name, questsData, playerController.transform.position, playerController.transform.rotation, currentlyTrackedQuest);
        string currentSaveJson = JsonConvert.SerializeObject(currentSave);
        using StreamWriter sw = new StreamWriter(path);
        sw.Write(currentSaveJson);
    }

    public void Load(string name)
    {
        string path = Path.Combine(savesPath, name + ".json");
        if (!File.Exists(path))
        {
            Debug.Log("Plik zapisu nie istnieje.");
            return;
        }
        using StreamReader sr = new StreamReader(path);
        string currentSaveJson = sr.ReadToEnd();
        currentSave = JsonConvert.DeserializeObject<SaveData>(currentSaveJson);
        currentSave.enabled = true;
        SceneManager.LoadScene(sceneName);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != sceneName)
        {
            return;
        }
        playerController = GameObject.FindGameObjectsWithTag("Player")[0];
        if (!currentSave.enabled)
        {
            return;
        }
        GameEventsManager.Instance.questEvents.QuestTrack(currentSave.currentlyTrackedQuest);
        playerController.transform.SetPositionAndRotation(JsonUtility.FromJson<Vector3>(currentSave.playerControllerPosition), JsonUtility.FromJson<Quaternion>(currentSave.playerControllerRotation));
        GameModeManager.Instance.ForceResume();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
}