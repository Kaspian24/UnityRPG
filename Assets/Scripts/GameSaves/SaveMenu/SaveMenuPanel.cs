using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlls load menu panel.
/// </summary>
public class SaveMenuPanel : MonoBehaviour
{
    public TMP_InputField newSaveInput;
    public Button newSaveButton;
    public GameObject saveListPanel;
    public GameObject saveListPanelItemPrefab;
    public Button backButton;

    private List<SaveData> saveDataList = new List<SaveData>();
    private List<GameObject> instantiatedSaveListItems = new List<GameObject>();

    private string savesPath;
    private Color saveInputDefaultColor;

    /// <summary>
    /// Links go buttons with functions. Gets saves path from save manager.
    /// </summary>
    private void Awake()
    {
        savesPath = SaveManager.Instance.savesPath;
        saveInputDefaultColor = newSaveInput.textComponent.color;
        newSaveButton.onClick.AddListener(() =>
        {
            if (!SaveManager.Instance.Save(newSaveInput.text, false))
            {
                newSaveInput.textComponent.color = Color.red;
            }
            ReloadSaveMenu();
        });
        backButton.onClick.AddListener(() => { GameModeManager.Instance.SwitchToLastGameMode(); });
        newSaveInput.onValueChanged.AddListener((string text) =>
        {
            newSaveInput.textComponent.color = saveInputDefaultColor;
        });
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
    /// Reloads saves in save menu.
    /// </summary>
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

    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        ReloadSaveMenu();

        GameEventsManager.Instance.gameModeEvents.OnReloadSaveMenu += ReloadSaveMenu;
    }

    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        ClearInstantiated(instantiatedSaveListItems);

        GameEventsManager.Instance.gameModeEvents.OnReloadSaveMenu -= ReloadSaveMenu;
    }
}
