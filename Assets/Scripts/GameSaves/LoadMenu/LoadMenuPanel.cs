using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlls load menu panel.
/// </summary>
public class LoadMenuPanel : MonoBehaviour
{
    public GameObject saveListPanel;
    public GameObject saveListPanelItemPrefab;
    public Button backButton;

    private List<SaveData> saveDataList = new List<SaveData>();
    private List<GameObject> instantiatedSaveListItems = new List<GameObject>();

    private string savesPath;

    /// <summary>
    /// Links go back button with function to switch to the last game mode. Gets saves path from save manager.
    /// </summary>
    private void Awake()
    {
        savesPath = SaveManager.Instance.savesPath;
        backButton.onClick.AddListener(() => { GameModeManager.Instance.SwitchToLastGameMode(); });
    }

    /// <summary>
    /// Clears list of instatniated game objects.
    /// </summary>
    /// <param name="gameObjects">Game object list to clear.</param>
    private void ClearInstantiated(List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        gameObjects.Clear();
    }

    /// <summary>
    /// Reloads saves in load menu.
    /// </summary>
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

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        ReloadLoadMenu();

        GameEventsManager.Instance.gameModeEvents.OnReloadLoadMenu += ReloadLoadMenu;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        ClearInstantiated(instantiatedSaveListItems);

        GameEventsManager.Instance.gameModeEvents.OnReloadLoadMenu -= ReloadLoadMenu;
    }
}
