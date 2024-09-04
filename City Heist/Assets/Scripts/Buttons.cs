using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public GameObject rulesPanel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        PlayerPrefs.DeleteAll();

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
        //StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
        rulesPanel.SetActive(true);
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
        //Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void EnableMinimap(bool b)
    {
        Debug.Log(b);
        if (b)
        {
            PlayerPrefs.SetInt("EnableMinimap", 0);
        }
        else if (!b)
        {
            PlayerPrefs.SetInt("EnableMinimap", 1);
        }
    }
}
