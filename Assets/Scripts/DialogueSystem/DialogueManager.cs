using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages dialogues.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialoguePanel;

    private GameObject dialogueHistory;

    private TextMeshProUGUI dialogueHistoryItemPrefab;

    private TextMeshProUGUI dialogueText;

    private GameObject dialogueChoices;

    private Button dialogueChoicePrefab;

    private Button continueButtonPrefab;

    private Story story;

    private List<GameObject> instantiatedHistoryItems = new List<GameObject>();

    private List<GameObject> instantiatedChoices = new List<GameObject>();

    private HashSet<(string, string)> enabledTopics = new HashSet<(string, string)>();

    TextAsset[] randomDialogues;

    private bool madeChoice;

    public Color playerColor = Color.blue;

    public Color npcColor = Color.white;

    /// <summary>
    /// Disables dialogue panel.
    /// </summary>
    private void Start()
    {
        dialoguePanel.SetActive(false);
    }
    /// <summary>
    /// Adds topic to enabled topics set.
    /// </summary>
    /// <param name="npcName">Name of the NPC.</param>
    /// <param name="conversationTopic">Topic of the conversation.</param>
    private void EnableTopic(string npcName, string conversationTopic)
    {
        enabledTopics.Add((npcName, conversationTopic));
    }
    /// <summary>
    /// Removes topic from enabled topics set.
    /// </summary>
    /// <param name="npcName">Name of the NPC.</param>
    /// <param name="conversationTopic">Topic of the conversation.</param>
    private void DisableTopic(string npcName, string conversationTopic)
    {
        enabledTopics.Remove((npcName, conversationTopic));
    }
    /// <summary>
    /// Initializes singleton, loads random dialogues.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        dialogueHistory = dialoguePanel.GetComponent<DialoguePanel>().dialogueHistory;

        dialogueHistoryItemPrefab = dialoguePanel.GetComponent<DialoguePanel>().dialogueHistoryItemPrefab;

        dialogueText = dialoguePanel.GetComponent<DialoguePanel>().dialogueText;

        dialogueChoices = dialoguePanel.GetComponent<DialoguePanel>().dialogueChoices;

        dialogueChoicePrefab = dialoguePanel.GetComponent<DialoguePanel>().dialogueChoicePrefab;

        continueButtonPrefab = dialoguePanel.GetComponent<DialoguePanel>().continueButtonPrefab;

        enabledTopics = SaveManager.Instance.currentSave.enabledTopics;

        randomDialogues = Resources.LoadAll<TextAsset>("Dialogues/Random");
    }
    /// <summary>
    /// Subscribes to events.
    /// </summary>
    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.OnEnableTopic += EnableTopic;
        GameEventsManager.Instance.dialogueEvents.OnDisableTopic += DisableTopic;

        GameEventsManager.Instance.gameModeEvents.OnToggleDialogue += ToggleDialogue;
    }
    /// <summary>
    /// Unsubscribes from events.
    /// </summary>
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.OnEnableTopic -= EnableTopic;
        GameEventsManager.Instance.dialogueEvents.OnDisableTopic -= DisableTopic;

        GameEventsManager.Instance.gameModeEvents.OnToggleDialogue -= ToggleDialogue;
    }
    /// <summary>
    /// Starts dialogue, calls for dialogue panel to show, binds functions to dialogue, displays first part of dialogue.
    /// </summary>
    /// <param name="inkJson"></param>
    public void StartDialogue(TextAsset inkJson)
    {
        madeChoice = false;

        dialogueText.color = npcColor;

        GameEventsManager.Instance.gameModeEvents.DialogueStartEnd(true);

        story = new Story(inkJson.text);

        BindExternalFunctions();

        ToggleDialogue(true);

        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
            DisplayChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    /// <summary>
    /// Starts random dialogue.
    /// </summary>
    /// <param name="dialoguesPath">Path to dialogue files.</param>
    public void StartRandomDialogue(string dialoguesPath = "")
    {
        TextAsset randomDialogue = randomDialogues[Random.Range(0, randomDialogues.Length)];
        StartDialogue(randomDialogue);
    }

    /// <summary>
    /// Binds external functions to dialogue.
    /// </summary>
    private void BindExternalFunctions()
    {
        story.BindExternalFunction("startQuest", (string questId) =>
        {
            GameEventsManager.Instance.questEvents.QuestStart(questId);
        });

        story.BindExternalFunction("isQuestStartable", (string questId) =>
        {
            return QuestManager.Instance.GetQuestById(questId).state == QuestState.CanStart;
        });

        story.BindExternalFunction("topicTalkedAbout", (string npcName, string conversationTopic) =>
        {
            GameEventsManager.Instance.dialogueEvents.TopicTalkedAbout(npcName, conversationTopic);
        });

        story.BindExternalFunction("isDialogueStartable", (string npcName, string conversationTopic) =>
        {
            bool test = enabledTopics.Contains((npcName, conversationTopic));
            return test;
        });

        story.BindExternalFunction("enableTopic", (string npcName, string conversationTopic) =>
        {
            GameEventsManager.Instance.dialogueEvents.EnableTopic(npcName, conversationTopic);
        });

        story.BindExternalFunction("disableTopic", (string npcName, string conversationTopic) =>
        {
            GameEventsManager.Instance.dialogueEvents.DisableTopic(npcName, conversationTopic);
        });
    }

    /// <summary>
    /// Shows new dialogue lines, displays dialogue choices, ends if can't continue dialogue.
    /// </summary>
    private void ContinueStory()
    {
        if (story.canContinue)
        {
            ClearInstantiated(instantiatedChoices);
            TextMeshProUGUI dialogueHistoryItem = Instantiate(dialogueHistoryItemPrefab, dialogueHistory.transform);
            dialogueHistoryItem.text = story.currentText;
            if (dialogueText.color == playerColor)
            {
                dialogueHistoryItem.color = playerColor;
            }
            instantiatedHistoryItems.Add(dialogueHistoryItem.gameObject);
            dialogueText.text = story.Continue();
            if (madeChoice)
            {
                dialogueText.color = playerColor;
                madeChoice = false;
            }
            else
            {
                dialogueText.color = npcColor;
            }
            DisplayChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    /// <summary>
    /// Ends dialogue, calls for dialogue panel to hide.
    /// </summary>
    private void EndDialogue()
    {
        ToggleDialogue(false);
        dialogueText.text = "";
        ClearInstantiated(instantiatedHistoryItems);
        ClearInstantiated(instantiatedChoices);
        GameEventsManager.Instance.gameModeEvents.DialogueStartEnd(false);
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
    /// Displays dialogue choices, continue button or end dialogue button. 
    /// </summary>
    private void DisplayChoices()
    {
        List<Choice> choices = story.currentChoices;
        int buttonIndex = 0;

        foreach (Choice choice in choices)
        {
            Button dialogueChoice = Instantiate(dialogueChoicePrefab, dialogueChoices.transform);
            dialogueChoice.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            int buttonIndexCopy = buttonIndex;
            dialogueChoice.onClick.AddListener(delegate () { MakeChoice(buttonIndexCopy); ContinueStory(); ContinueStory(); });
            instantiatedChoices.Add(dialogueChoice.gameObject);
            buttonIndex++;
        }
        if (instantiatedChoices.Count == 0)
        {
            Button continueDialog = Instantiate(continueButtonPrefab, dialogueChoices.transform);
            continueDialog.onClick.AddListener(delegate () { ContinueStory(); });
            instantiatedChoices.Add(continueDialog.gameObject);
            if (!story.canContinue)
            {
                continueDialog.GetComponentInChildren<TextMeshProUGUI>().text = "Zakoñcz";
            }
        }
        instantiatedChoices[0].GetComponent<Button>().Select();
        dialoguePanel.GetComponent<DialoguePanel>().ScrollToBottom();
    }

    /// <summary>
    /// Makes a dialogue choice.
    /// </summary>
    /// <param name="choiceIndex">Dialogue choice index.</param>
    public void MakeChoice(int choiceIndex)
    {
        madeChoice = true;
        story.ChooseChoiceIndex(choiceIndex);
    }

    /// <summary>
    /// Toggles dialogue panel.
    /// </summary>
    /// <param name="state">Panel state. True to show, false to hide.</param>
    public void ToggleDialogue(bool state)
    {
        dialoguePanel.SetActive(state);
    }

    /// <summary>
    /// Getter for enabled topics set.
    /// </summary>
    /// <returns>Enabled topics set.</returns>
    public HashSet<(string, string)> SaveEnabledTopics()
    {
        return enabledTopics;
    }
}
