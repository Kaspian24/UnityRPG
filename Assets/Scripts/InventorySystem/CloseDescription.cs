using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseDescription : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject descriptionPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }
}
