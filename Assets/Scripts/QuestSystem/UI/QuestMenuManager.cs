using UnityEngine;

public class QuestMenuManager : MonoBehaviour
{
    bool paused;

    public GameObject questMenuPanel;

    public GameObject PlayerController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Pause()
    {
        paused = true;
        questMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        PlayerController.GetComponent<FirstPersonController>().cameraCanMove = false;
    }

    private void Resume()
    {
        paused = false;
        questMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerController.GetComponent<FirstPersonController>().cameraCanMove = true;
    }
}
