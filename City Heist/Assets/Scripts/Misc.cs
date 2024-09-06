using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Misc : MonoBehaviour
{
    static float money=1000;
    [SerializeField] TMP_Text moneytext;
    [SerializeField] GameObject taskmenu;

    // Countdown Timer
    public float remainingTime = 600f;
    public bool isTimerRunning = true;
    public TMP_Text countdownTimerText;

    // Pause
    [HideInInspector]
    public bool isPaused = false;
    public GameObject pausePanel;
    public Driving playerVehicle;
    public GameObject staticRulesPanel;
    public TMP_Dropdown qualityDropdown;

    //add reference to the AudioSource and AudioClip
    public AudioSource buttonAudioSource;
    public AudioClip buttonSound;

    //create function to play the sound
    private void PlayButtonSound()
    {
        if (buttonAudioSource != null && buttonSound != null)
        {
            buttonAudioSource.PlayOneShot(buttonSound);
        }
    }

    public void Deduct() 
    {
        money = money - 10;
        if (money < 0)
        {
            money = 0;
        }
        PlayerPrefs.SetFloat("Money", money);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        money = PlayerPrefs.GetFloat("Money", 1000f);
        PlayerPrefs.SetFloat("Money", money);
		if (PlayerPrefs.GetInt("EnableMinimap", 1) == 0)
		{
			remainingTime = PlayerPrefs.GetFloat("Time", 540f);
		}
		else
		{
			remainingTime = PlayerPrefs.GetFloat("Time", 600f);
		}
		
        if (qualityDropdown != null)
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
        }
    }

    private void Update()
    {
        moneytext.text = "$ " + money;

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (taskmenu.activeSelf)
            {
                taskmenu.SetActive(false);

            }
            else
            {
                taskmenu.SetActive(true);

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        // Countdown Timer
        if (isTimerRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                DisplayRemainingTime(remainingTime);
            }
            else
            {
                // Lose
                Debug.Log("Time has run out!");
                remainingTime = 0;
                isTimerRunning = false;
                StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
            }
        }
    }
    void DisplayRemainingTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Pause()
    {
        //play the button sound
        PlayButtonSound();

        if (staticRulesPanel.activeSelf)
        {
            return;
        }

        pausePanel.SetActive(!pausePanel.activeSelf);
        if (!isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            playerVehicle.enabled = false;
        }
        else if (isPaused)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            playerVehicle.enabled = true;
        }
        isPaused = !isPaused;
    }

    public void ReturnToMainMenu()
    {
        Pause();
        StartCoroutine(SceneTransition.instance.TransitionToScene("MainMenu"));
    }

    public void ShowRules()
    {
        //play the button sound
        PlayButtonSound();

        staticRulesPanel.SetActive(true);
    }

    public void CloseRules()
    {
        //play the button sound
        PlayButtonSound();

        staticRulesPanel.SetActive(false);
    }

    public void SetQuality(int qualityIndex)
    {
        //play the button sound
        PlayButtonSound();

        QualitySettings.SetQualityLevel(qualityIndex);
        //Debug.Log(QualitySettings.GetQualityLevel());
    }
}
