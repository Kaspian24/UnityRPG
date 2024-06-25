using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private GameObject playerController;

    [HideInInspector]
    public SaveData currentSave = new SaveData();

    public string savesPath = "Assets/Resources/Saves";

    public Object gameScene;

    public Object menuScene;

    private string gameSceneName;

    private string menuSceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameSceneName = gameScene.name;
        menuSceneName = menuScene.name;
    }

    public bool Save(string name, bool canOverride)
    {
        Directory.CreateDirectory(savesPath);
        string path = Path.Combine(savesPath, name + ".json");
        if (File.Exists(path) && !canOverride)
        {
            Debug.Log("Plik zapisu ju¿ istnieje.");
            return false;
        }
        Dictionary<string, QuestData> questsData = QuestManager.Instance.SaveQuests();
        string currentlyTrackedQuest = QuestMenuManager.Instance.GetCurrentlyTrackedQuest();
        List<KeyValuePair<string, int>> items = GameObject.FindGameObjectsWithTag("InventoryCanvas")[0].GetComponent<InventoryManager>().SaveItems();
        List<KeyValuePair<string, string>> equippedItems = GameObject.FindGameObjectsWithTag("InventoryCanvas")[0].GetComponent<InventoryManager>().SaveEquippedItems();
        Dictionary<string, int> stats = GameObject.FindGameObjectsWithTag("InventoryCanvas")[0].GetComponent<InventoryManager>().saveStats();
        long currentTime = System.DateTime.Now.ToBinary();
        playerController = GameObject.FindGameObjectsWithTag("Player")[0];
        currentSave = new SaveData(name, questsData, playerController.transform.position, playerController.transform.rotation, currentlyTrackedQuest, items, equippedItems, stats, currentTime);
        string currentSaveJson = JsonConvert.SerializeObject(currentSave);
        using StreamWriter sw = new StreamWriter(path);
        sw.Write(currentSaveJson);
        return true;
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
        SceneManager.LoadScene(gameSceneName);
    }

    public void Delete(string name)
    {
        string path = Path.Combine(savesPath, name + ".json");
        if (!File.Exists(path))
        {
            Debug.Log("Plik zapisu nie istnieje.");
            return;
        }
        File.Delete(path);
    }

    public void NewGame()
    {
        currentSave = new SaveData();
        SceneManager.LoadScene(gameSceneName);
    }

    public void MainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(menuSceneName);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != gameSceneName)
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
