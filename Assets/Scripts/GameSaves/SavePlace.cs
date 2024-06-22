using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlace : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameEventsManager.Instance.gameModeEvents.SaveMenuOnOff(true);
    }
}
