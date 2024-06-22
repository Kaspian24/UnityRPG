using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveListPanelItem : MonoBehaviour
{
    public Button saveButton;
    public Button deleteButton;
    public TMP_Text saveName;
    public TMP_Text saveDate;

    public void SetSave(string name, long date)
    {
        saveName.text = name;
        saveDate.text = System.DateTime.FromBinary(date).ToLocalTime().ToString();
        saveButton.onClick.AddListener(() => { _ = SaveManager.Instance.Save(name, true); GameEventsManager.Instance.gameModeEvents.ReloadSaveMenu(); });
        deleteButton.onClick.AddListener(() => { SaveManager.Instance.Delete(name); GameEventsManager.Instance.gameModeEvents.ReloadSaveMenu(); });
    }

    public void SetLoad(string name, long date)
    {
        saveName.text = name;
        saveDate.text = System.DateTime.FromBinary(date).ToLocalTime().ToString();
        saveButton.onClick.AddListener(() => SaveManager.Instance.Load(name));
        deleteButton.onClick.AddListener(() => { SaveManager.Instance.Delete(name); GameEventsManager.Instance.gameModeEvents.ReloadLoadMenu(); });
    }
}
