using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    public GameObject dialogueHistory;

    public TextMeshProUGUI dialogueHistoryItemPrefab;

    public TextMeshProUGUI dialogueText;

    public GameObject dialogueChoices;

    public Button dialogueChoicePrefab;

    public Button continueButtonPrefab;

    public ScrollRect scrollRect;

    public void ScrollToBottom()
    {
        StartCoroutine(ForceScrollDown());
    }

    IEnumerator ForceScrollDown()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}
