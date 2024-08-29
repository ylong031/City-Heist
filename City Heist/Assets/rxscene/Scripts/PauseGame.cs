using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static PauseGame instance;
    [HideInInspector]
    public bool isPaused = false;
    public GameObject pausePanel;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.cctvPanel.activeSelf || GameManager.instance.vaultKeypad.activeSelf || GameManager.instance.colourSquarePanel.activeSelf || GameManager.instance.instructionsPanel.activeSelf || GameManager.instance.escapePanel.activeSelf || GameManager.instance.staticRulesPanel.activeSelf)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    void Pause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (!isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            GameManager.instance.isTimerRunning = false;
            GameManager.instance.thirdPersonCamera.enabled = false;
            GameManager.instance.playerMovement.enabled = false;
        }
        else if (isPaused)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.instance.isTimerRunning = true;
            GameManager.instance.thirdPersonCamera.enabled = true;
            GameManager.instance.playerMovement.enabled = true;
        }
        isPaused = !isPaused;
    }

    public void ReturnToMainMenu()
    {
        Pause();
        StartCoroutine(SceneTransition.instance.TransitionToScene("MainMenu"));
    }
}
