using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Dropdown qualityDropdown;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        if (moneyText != null && PlayerPrefs.GetFloat("Money") != 0)
        {
            moneyText.text = "$" + PlayerPrefs.GetFloat("Money");
        }
        PlayerPrefs.SetFloat("Time", 600f);
        PlayerPrefs.SetInt("NextBank", 0);
        PlayerPrefs.SetInt("isColourSquareTask", -1);

        if(qualityDropdown != null)
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
        }
        PlayerPrefs.SetInt("EnableMinimap", 1);
    }

    public void Play() 
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit() 
    {
        Debug.Log("Quitting......");
        Application.Quit();
    }

    public void Rules() 
    {
        Debug.Log("Rules!");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void EnableMinimap(bool b)
    {
        Debug.Log(b);
        if (b)
        {
            PlayerPrefs.SetInt("EnableMinimap", 1);
        }
        else if (!b)
        {
            PlayerPrefs.SetInt("EnableMinimap", 0);
        }
    }
}
