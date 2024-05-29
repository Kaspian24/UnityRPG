using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvents
{
    public event Action<string, string> OnTopicTalkedAbout; // subscribe to this to react on topic being talked about
    public void TopicTalkedAbout(string npcName, string conversationTopic) // call this to notify about topic being talked about
    {
        OnTopicTalkedAbout?.Invoke(npcName, conversationTopic);
    }
    public event Action<string, string> OnEnableTopic; // managed by DialogueManager
    public void EnableTopic(string npcName, string conversationTopic) // call this to enable topic
    {
        OnEnableTopic?.Invoke(npcName, conversationTopic);
    }
    public event Action<string, string> OnDisableTopic; // managed by DialogueManager
    public void DisableTopic(string npcName, string conversationTopic) // call this to disable topic
    {
        OnDisableTopic?.Invoke(npcName, conversationTopic);
    }
}
