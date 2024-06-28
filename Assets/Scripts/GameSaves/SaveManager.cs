using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages save system.
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private GameObject playerController;

    [HideInInspector]
    public SaveData currentSave = new SaveData();

    public string savesPath = "Assets/Resources/Saves";

    public Object gameScene;

    public Object menuScene;

    private int gameSceneIndex = 1;

    private int menuSceneIndex = 0;

    /// <summary>
    /// Initializes singleton and prevents destruction of the game object on load.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Creates a save file.
    /// </summary>
    /// <param name="name">Save name.</param>
    /// <param name="canOverride">Decides if save can be overwritten on name conflict.</param>
    /// <returns>true if save was successfull, false otherwise.</returns>
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
        HashSet<(string, string)> enabledTopics = DialogueManager.Instance.SaveEnabledTopics();
        long currentTime = System.DateTime.Now.ToBinary();
        playerController = GameObject.FindGameObjectsWithTag("Player")[0];
        currentSave = new SaveData(name, questsData, playerController.transform.position, playerController.transform.rotation, currentlyTrackedQuest, items, equippedItems, stats, enabledTopics, currentTime);
        string currentSaveJson = JsonConvert.SerializeObject(currentSave);
        using StreamWriter sw = new StreamWriter(path);
        sw.Write(currentSaveJson);
        return true;
    }

    /// <summary>
    /// Loads save from a file.
    /// </summary>
    /// <param name="name">Save name.</param>
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
        SceneManager.LoadScene(gameSceneIndex);
    }

    /// <summary>
    /// Deletes save file.
    /// </summary>
    /// <param name="name">Save name.</param>
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

    /// <summary>
    /// Sets current save to an empty save and loads game scene.
    /// </summary>
    public void NewGame()
    {
        currentSave = new SaveData();
        SceneManager.LoadScene(gameSceneIndex);
    }

    /// <summary>
    /// Loads main menu scene and unlocks cursor.
    /// </summary>
    public void MainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(menuSceneIndex);
    }

    /// <summary>
    /// Sets player position after scene was loaded.
    /// </summary>
    /// <param name="scene">Loaded scene.</param>
    /// <param name="mode">Load scene mode.</param>
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != gameSceneIndex)
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

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    /// <summary>
    /// Unsubscribes to events.
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
}
