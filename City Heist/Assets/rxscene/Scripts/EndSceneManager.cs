using System.Collections;
using TMPro;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    public TMP_Text moneyEarnedText;
    public TMP_Text timeLeftText;
    public TMP_Text gradeText;
    public Animator worldCanvasAnim;
    public Animator robberyVehicleAnim;
    public GameObject robberyVehicleSmoke;

    public GameObject losePanel;

    //add reference to the AudioSource and AudioClip
    public AudioSource buttonAudioSource;
    public AudioClip buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        if (PlayerPrefs.GetInt("GameLost", 0) == 1)
        {
            PlayerPrefs.DeleteAll();
            losePanel.SetActive(true);
            return;
        }

        float moneyEarned = PlayerPrefs.GetFloat("Money");
        moneyEarnedText.text = "Money Earned: $" + moneyEarned;

        float remainingTime = PlayerPrefs.GetFloat("Time");
        float minutes = Mathf.FloorToInt(remainingTime / 60);
        float seconds = Mathf.FloorToInt(remainingTime % 60);
        timeLeftText.text = "Time Left:\n" + minutes + "min " + seconds + "s";

        string grade = "";
        if (PlayerPrefs.GetInt("EnableMinimap", 1) == 0)
        {
            // 1000 + 2000 + 2000 + 1000 + (40-200)x16
            if (moneyEarned >= 7200)
            {
                grade = "S";
            }
            else if (moneyEarned >= 5000)
            {
                grade = "A";
            }
            else if (moneyEarned >= 3000)
            {
                grade = "B";
            }
            else if (moneyEarned >= 1000)
            {
                grade = "C";
            }
            else
            {
                grade = "D";
            }
        }
        else
        {
            // 1000 + 1000 + 1000 + 500 + (20-100)x16
            if (moneyEarned >= 4100)
            {
                grade = "S";
            }
            else if (moneyEarned >= 3000)
            {
                grade = "A";
            }
            else if (moneyEarned >= 2000)
            {
                grade = "B";
            }
            else if (moneyEarned >= 1000)
            {
                grade = "C";
            }
            else
            {
                grade = "D";
            }
        }
        gradeText.text = grade + " Grade";
    }

    //create function to play the sound
    private void PlayButtonSound()
    {
        if (buttonAudioSource != null && buttonSound != null)
        {
            buttonAudioSource.PlayOneShot(buttonSound);
        }
    }

    public void ReturnToMainMenu()
    {
        //play the button sound when button pressed
        PlayButtonSound();

        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        //play the button sound when button pressed
        PlayButtonSound();

        worldCanvasAnim.SetTrigger("Return");
        yield return new WaitForSeconds(3f);
        robberyVehicleSmoke.SetActive(true);
        yield return new WaitForSeconds(1f);
        robberyVehicleAnim.enabled = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(SceneTransition.instance.TransitionToScene("MainMenu"));
    }

    public void Retry()
    {
        //play the button sound when button pressed
        PlayButtonSound();

        StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
    }

    public void FastReturnToMainMenu()
    {
        //play the button sound when button pressed
        PlayButtonSound();

        StartCoroutine(SceneTransition.instance.TransitionToScene("MainMenu"));
    }
}
