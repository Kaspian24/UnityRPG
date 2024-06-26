using Ink.Runtime;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }
    private void EnableTopic(string npcName, string conversationTopic)
    {
        enabledTopics.Add((npcName, conversationTopic));
    }
    private void DisableTopic(string npcName, string conversationTopic)
    {
        enabledTopics.Remove((npcName, conversationTopic));
    }
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
    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.OnEnableTopic += EnableTopic;
        GameEventsManager.Instance.dialogueEvents.OnDisableTopic += DisableTopic;

        GameEventsManager.Instance.gameModeEvents.OnToggleDialogue += ToggleDialogue;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.OnEnableTopic -= EnableTopic;
        GameEventsManager.Instance.dialogueEvents.OnDisableTopic -= DisableTopic;

        GameEventsManager.Instance.gameModeEvents.OnToggleDialogue -= ToggleDialogue;
    }
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

    public void StartRandomDialogue(string dialoguesPath = "")
    {
        TextAsset randomDialogue = randomDialogues[Random.Range(0, randomDialogues.Length)];
        StartDialogue(randomDialogue);
    }

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

    private void ContinueStory()
    {
        if (story.canContinue)
        {
            ClearInstantiated(instantiatedChoices);
            TextMeshProUGUI dialogueHistoryItem = Instantiate(dialogueHistoryItemPrefab, dialogueHistory.transform);
            dialogueHistoryItem.text = story.currentText;
            if(dialogueText.color == playerColor)
            {
                dialogueHistoryItem.color = playerColor;
            }
            instantiatedHistoryItems.Add(dialogueHistoryItem.gameObject);
            dialogueText.text = story.Continue();
            if(madeChoice)
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

    private void EndDialogue()
    {
        ToggleDialogue(false);
        dialogueText.text = "";
        ClearInstantiated(instantiatedHistoryItems);
        ClearInstantiated(instantiatedChoices);
        GameEventsManager.Instance.gameModeEvents.DialogueStartEnd(false);
    }

    private void ClearInstantiated(List<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        gameObjects.Clear();
    }

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

    public void MakeChoice(int choiceIndex)
    {
        madeChoice = true;
        story.ChooseChoiceIndex(choiceIndex);
    }

    public void ToggleDialogue(bool state)
    {
        dialoguePanel.SetActive(state);
    }

    public HashSet<(string, string)> SaveEnabledTopics()
    {
        return enabledTopics;
    }
}
