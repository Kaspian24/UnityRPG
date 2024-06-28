using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlls dialogue panel. Provides access to it's elements.
/// </summary>
public class DialoguePanel : MonoBehaviour
{
    public GameObject dialogueHistory;

    public TextMeshProUGUI dialogueHistoryItemPrefab;

    public TextMeshProUGUI dialogueText;

    public GameObject dialogueChoices;

    public Button dialogueChoicePrefab;

    public Button continueButtonPrefab;

    public ScrollRect scrollRect;

    /// <summary>
    /// Scrolls to bottom of dialogue panel.
    /// </summary>
    public void ScrollToBottom()
    {
        StartCoroutine(ForceScrollDown());
    }

    /// <summary>
    /// Scrolls to bottom of dialogue panel at the end of frame.
    /// </summary>
    /// <returns>Ienumerator for a courutine.</returns>
    IEnumerator ForceScrollDown()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
