using UnityEngine;
using UnityEngine.UI;

public class MinimapMarkerManager : MonoBehaviour
{
    public GameObject markerPrefab;
    public RectTransform minimapImage;
    public Camera minimapCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 playerPosition = GetPlayerPosition();
            Vector2 minimapPosition = WorldToMinimap(playerPosition);
            AddMarker(minimapPosition);
        }
    }

    Vector3 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    Vector2 WorldToMinimap(Vector3 worldPosition)
    {
        Vector3 viewportPosition = minimapCamera.WorldToViewportPoint(worldPosition);
        RectTransform canvasRect = minimapImage.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        Vector2 minimapPosition = new Vector2(
            (viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
            (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)
        );

        return minimapPosition;
    }

    void AddMarker(Vector2 position)
    {
        GameObject newMarker = Instantiate(markerPrefab);
        newMarker.transform.SetParent(null);
        newMarker.GetComponent<RectTransform>().anchoredPosition = position;
    }
}
