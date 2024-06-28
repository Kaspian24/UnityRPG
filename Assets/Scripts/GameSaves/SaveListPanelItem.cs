using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlls single item on saves list.
/// </summary>
public class SaveListPanelItem : MonoBehaviour
{
    public Button saveButton;
    public Button deleteButton;
    public TMP_Text saveName;
    public TMP_Text saveDate;

    /// <summary>
    /// Sets visible text fields and adds listeners to save and delete buttons.
    /// </summary>
    /// <param name="name">Save name.</param>
    /// <param name="date">Save date.</param>
    public void SetSave(string name, long date)
    {
        saveName.text = name;
        saveDate.text = System.DateTime.FromBinary(date).ToLocalTime().ToString();
        saveButton.onClick.AddListener(() => { _ = SaveManager.Instance.Save(name, true); GameEventsManager.Instance.gameModeEvents.ReloadSaveMenu(); });
        deleteButton.onClick.AddListener(() => { SaveManager.Instance.Delete(name); GameEventsManager.Instance.gameModeEvents.ReloadSaveMenu(); });
    }

    /// <summary>
    /// Sets visible text fields and adds listeners to load and delete buttons.
    /// </summary>
    /// <param name="name">Save name.</param>
    /// <param name="date">Save date.</param>
    public void SetLoad(string name, long date)
    {
        saveName.text = name;
        saveDate.text = System.DateTime.FromBinary(date).ToLocalTime().ToString();
        saveButton.onClick.AddListener(() => SaveManager.Instance.Load(name));
        deleteButton.onClick.AddListener(() => { SaveManager.Instance.Delete(name); GameEventsManager.Instance.gameModeEvents.ReloadLoadMenu(); });
    }
}
