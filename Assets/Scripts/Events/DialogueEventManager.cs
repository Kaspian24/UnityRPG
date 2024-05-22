using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventManager
{
    public event Action<string, string> OnNpcTalkedTo; // subscribe to this to react on quest state change
    public void NpcTalkedTo(string npcName, string conversationTopic) // call this to start quest
    {
        OnNpcTalkedTo?.Invoke(npcName, conversationTopic);
    }
}
