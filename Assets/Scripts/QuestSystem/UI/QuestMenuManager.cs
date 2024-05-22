using UnityEngine;

public class QuestMenuManager : MonoBehaviour
{
    public static QuestMenuManager Instance { get; private set; }

    bool paused;

    public GameObject questMenuPanel;

    public GameObject PlayerController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        questMenuPanel.SetActive(false);
    }

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

    private void Pause() // to powinno byæ w osobnym menagerze stanu gry
    {
        paused = true;
        questMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        PlayerController.GetComponent<FirstPersonController>().enabled = false;
    }

    private void Resume() // to powinno byæ w osobnym menagerze stanu gry
    {
        paused = false;
        questMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerController.GetComponent<FirstPersonController>().enabled = true;
    }
}
