using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class that manages closing description window
/// </summary>
public class CloseDescription : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject descriptionPanel;

    /// <summary>
    /// Sets description panel to inactive when clicked
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }
}
