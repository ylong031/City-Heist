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

    public void Deduct() 
    {
        money = money - 10;
        PlayerPrefs.SetFloat("Money", money);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        money = PlayerPrefs.GetFloat("Money", 1000f);
        PlayerPrefs.SetFloat("Money", money);
        remainingTime = PlayerPrefs.GetFloat("Time", 600f);
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
}
