using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialoguePanel;

    public GameObject dialogueHistory;

    public TextMeshProUGUI dialogueHistoryItemPrefab;

    public TextMeshProUGUI dialogueText;

    public GameObject dialogueChoices;

    public Button dialogueChoicePrefab;

    public Button continueButtonPrefab;

    public GameObject PlayerController;

    private Story story;

    private List<GameObject> instantiatedHistoryItems = new List<GameObject>();

    private List<GameObject> instantiatedChoices = new List<GameObject>();

    private HashSet<(string, string)> enabledTopics = new HashSet<(string, string)>();

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
        Instance = this;
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.OnEnableTopic += EnableTopic;
        GameEventsManager.Instance.dialogueEvents.OnDisableTopic += DisableTopic;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.OnEnableTopic -= EnableTopic;
        GameEventsManager.Instance.dialogueEvents.OnDisableTopic -= DisableTopic;
    }
    public void StartDialogue(TextAsset inkJson)
    {
        Pause();

        story = new Story(inkJson.text);

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

        dialoguePanel.SetActive(true);

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

    private void ContinueStory()
    {
        if (story.canContinue)
        {
            ClearInstantiated(instantiatedChoices);
            TextMeshProUGUI dialogueHistoryItem = Instantiate(dialogueHistoryItemPrefab, dialogueHistory.transform);
            dialogueHistoryItem.text = story.currentText;
            instantiatedHistoryItems.Add(dialogueHistoryItem.gameObject);
            dialogueText.text = story.Continue();
            DisplayChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        ClearInstantiated(instantiatedHistoryItems);
        ClearInstantiated(instantiatedChoices);
        Resume();
    }

    private void Pause() // to powinno byæ w osobnym menagerze stanu gry
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        PlayerController.GetComponent<FirstPersonController>().enabled = false;
    }

    private void Resume() // to powinno byæ w osobnym menagerze stanu gry
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerController.GetComponent<FirstPersonController>().enabled = true;
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
        }
        instantiatedChoices[0].GetComponent<Button>().Select();
    }

    public void MakeChoice(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
    }
}
