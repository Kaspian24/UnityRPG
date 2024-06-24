using UnityEngine;

public class PrintMessage : MonoBehaviour
{
    public GameObject markerPrefab;

    Vector3 GetPlayerPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update()
    {
        if (GameModeManager.Instance.currentGameMode == GameMode.Gameplay && Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 playerWorldPosition = GetPlayerPosition();
            GameObject newMarker = Instantiate(markerPrefab);
            newMarker.transform.position = playerWorldPosition;
            Renderer markerRenderer = newMarker.GetComponent<Renderer>();

            if (markerRenderer != null)
            {
                markerRenderer.material.color = Color.red;
            }
        }
    }
}
