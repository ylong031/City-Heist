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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        float moneyEarned = PlayerPrefs.GetFloat("Money");
        moneyEarnedText.text = "Money Earned: $" + moneyEarned;

        float remainingTime = PlayerPrefs.GetFloat("Time");
        float minutes = Mathf.FloorToInt(remainingTime / 60);
        float seconds = Mathf.FloorToInt(remainingTime % 60);
        timeLeftText.text = "Time Left:\n" + minutes + "min " + seconds + "s";

        string grade = "";
        if (moneyEarned >= 4000)
        {
            grade = "S+";
        }
        else if (moneyEarned >= 3000)
        {
            grade = "S";
        }
        else if (moneyEarned >= 2000)
        {
            grade = "A";
        }
        else if (moneyEarned >= 1000)
        {
            grade = "B";
        }
        else if (moneyEarned >= 0)
        {
            grade = "C";
        }
        else if (moneyEarned < 0)
        {
            grade = "D";
        }
        gradeText.text = grade + " Grade";
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(Return());
    }

    IEnumerator Return()
    {
        worldCanvasAnim.SetTrigger("Return");
        yield return new WaitForSeconds(3f);
        robberyVehicleSmoke.SetActive(true);
        yield return new WaitForSeconds(1f);
        robberyVehicleAnim.enabled = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(SceneTransition.instance.TransitionToScene("MainMenu"));
    }
}
