using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public GameObject MainMenuPanel;

    public GameObject LoadMenuPanel;

    public Button NewGameButton;

    public Button LoadGameButton;

    public Button QuitButton;

    public Button GoBackButton;

    public GameObject saveListPanel;

    public GameObject saveListPanelItemPrefab;

    private List<SaveData> saveDataList = new List<SaveData>();
    private List<GameObject> instantiatedSaveListItems = new List<GameObject>();

    private string savesPath;

    private void Awake()
    {
        MainMenuPanel.SetActive(true);
        LoadMenuPanel.SetActive(false);

        NewGameButton.onClick.AddListener(() => { SaveManager.Instance.NewGame(); });
        LoadGameButton.onClick.AddListener(() => { MainMenuPanel.SetActive(false); LoadMenuPanel.SetActive(true); ReloadLoadMenu(); });
        QuitButton.onClick.AddListener(() => { Application.Quit(); Debug.Log("Apllication Quit"); });
        GoBackButton.onClick.AddListener(() => { LoadMenuPanel.SetActive(false); MainMenuPanel.SetActive(true); });

        savesPath = SaveManager.Instance.savesPath;
    }

    private void ClearInstantiated(List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        gameObjects.Clear();
    }

    private void ReloadLoadMenu()
    {
        ClearInstantiated(instantiatedSaveListItems);
        string[] saveFiles = Directory.GetFiles(savesPath, "*.json");
        foreach (string file in saveFiles)
        {
            using StreamReader sr = new StreamReader(file);
            string saveString = sr.ReadToEnd();
            SaveData save = JsonConvert.DeserializeObject<SaveData>(saveString);
            save.enabled = true;
            saveDataList.Add(save);
        }
        saveDataList.Sort((a, b) => b.saveDate.CompareTo(a.saveDate));
        foreach (SaveData saveData in saveDataList)
        {
            GameObject saveListPanelItem = Instantiate(saveListPanelItemPrefab, saveListPanel.transform);
            saveListPanelItem.GetComponent<SaveListPanelItem>().SetLoad(saveData.saveName, saveData.saveDate);
            instantiatedSaveListItems.Add(saveListPanelItem);
        }
        saveDataList.Clear();
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.gameModeEvents.OnReloadLoadMenu += ReloadLoadMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.gameModeEvents.OnReloadLoadMenu -= ReloadLoadMenu;
    }
}
