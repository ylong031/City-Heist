using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public GameObject rulesPanel;

    //add reference to the AudioSource and AudioClip
    public AudioSource buttonAudioSource;
    public AudioClip buttonSound;

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

    //create function to play the sound
    private void PlayButtonSound()
    {
        if (buttonAudioSource != null && buttonSound != null)
        {
            buttonAudioSource.PlayOneShot(buttonSound);
        }
    }

    public void Play() 
    {
        //play the button sound when button pressed
        PlayButtonSound();

        //SceneManager.LoadScene(1);
        //StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
        rulesPanel.SetActive(true);
    }
    public void Exit() 
    {
        //play the button sound when button pressed
        PlayButtonSound();

        Debug.Log("Quitting......");
        Application.Quit();
    }

    public void Rules() 
    {
        //play the button sound when button pressed
        PlayButtonSound();

        Debug.Log("Rules!");
    }

    public void SetQuality(int qualityIndex)
    {
        //play the button sound when button pressed
        PlayButtonSound();

        QualitySettings.SetQualityLevel(qualityIndex);
        //Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void EnableMinimap(bool b)
    {
        //play the button sound when button pressed
        PlayButtonSound();

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
