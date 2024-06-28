using System;

/// <summary>
/// Class with events related to dialogues.
/// </summary>
public class DialogueEvents
{
    /// <summary>
    /// Triggered when topic is talked about
    /// </summary>
    public event Action<string, string> OnTopicTalkedAbout;

    /// <summary>
    /// Invokes <see cref="OnTopicTalkedAbout"/> event.
    /// </summary>
    /// <param name="npcName">Name of the npc.</param>
    /// <param name="conversationTopic">Topic of the conversation.</param>
    public void TopicTalkedAbout(string npcName, string conversationTopic)
    {
        OnTopicTalkedAbout?.Invoke(npcName, conversationTopic);
    }

    /// <summary>
    /// Triggered when topic is enabled.
    /// </summary>
    public event Action<string, string> OnEnableTopic;

    /// <summary>
    /// Invokes <see cref="OnEnableTopic"/> event.
    /// </summary>
    /// <param name="npcName">Name of the npc.</param>
    /// <param name="conversationTopic">Topic of the conversation.</param>
    public void EnableTopic(string npcName, string conversationTopic)
    {
        OnEnableTopic?.Invoke(npcName, conversationTopic);
    }

    /// <summary>
    /// Triggered when topic is disabled.
    /// </summary>
    public event Action<string, string> OnDisableTopic;

    /// <summary>
    /// Invokes <see cref="OnDisableTopic"/> event.
    /// </summary>
    /// <param name="npcName">Name of the npc.</param>
    /// <param name="conversationTopic">Topic of the conversation.</param>
    public void DisableTopic(string npcName, string conversationTopic)
    {
        OnDisableTopic?.Invoke(npcName, conversationTopic);
    }
}
