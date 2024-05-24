using UnityEngine;
using UnityEngine.UI;

public class MinimapMarkerManager : MonoBehaviour
{
    public GameObject markerPrefab;
    public RectTransform minimapImage;
    public Camera minimapCamera;

    void Start()
    {
        Initialize(GetPlayerPosition());
    }

    public void Initialize(Vector3 playerPosition)
    {
        Vector2 minimapPosition = WorldToMinimap(playerPosition);
        AddMarker(minimapPosition);
    }

    Vector3 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    Vector2 WorldToMinimap(Vector3 worldPosition)
    {
        Vector3 viewportPosition = minimapCamera.WorldToViewportPoint(worldPosition);
        
        float x = (viewportPosition.x * minimapImage.rect.width) - (minimapImage.rect.width * 0.5f);
        float y = (viewportPosition.y * minimapImage.rect.height) - (minimapImage.rect.height * 0.5f);

        return new Vector2(x, y);
    }

    void AddMarker(Vector2 position)
    {
        GameObject newMarker = Instantiate(markerPrefab);
        newMarker.transform.SetParent(minimapImage, false);
        newMarker.GetComponent<RectTransform>().anchoredPosition = position;
    }
}
