using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenuPanel : MonoBehaviour
{
    public TMP_InputField newSaveInput;
    public Button newSaveButton;
    public GameObject saveListPanel;
    public GameObject saveListPanelItemPrefab;

    private List<SaveData> saveDataList = new List<SaveData>();
    private List<GameObject> instantiatedSaveListItems = new List<GameObject>();

    private string savesPath;

    private void Awake()
    {
        savesPath = SaveManager.Instance.savesPath;
        newSaveButton.onClick.AddListener(() => { SaveManager.Instance.Save(newSaveInput.text, false); ReloadSaveMenu(); });
    }

    private void ClearInstantiated(List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        gameObjects.Clear();
    }

    private void ReloadSaveMenu()
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
            saveListPanelItem.GetComponent<SaveListPanelItem>().SetSave(saveData.saveName, saveData.saveDate);
            instantiatedSaveListItems.Add(saveListPanelItem);
        }
        saveDataList.Clear();
    }

    private void OnEnable()
    {
        ReloadSaveMenu();

        GameEventsManager.Instance.gameModeEvents.OnReloadSaveMenu += ReloadSaveMenu;
    }

    private void OnDisable()
    {
        ClearInstantiated(instantiatedSaveListItems);

        GameEventsManager.Instance.gameModeEvents.OnReloadSaveMenu -= ReloadSaveMenu;
    }
}
