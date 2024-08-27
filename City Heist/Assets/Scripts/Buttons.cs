using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public TMP_Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        if (moneyText != null)
        {
            moneyText.text = "$" + PlayerPrefs.GetFloat("Money");
        }
        PlayerPrefs.SetFloat("Time", 600f);
        PlayerPrefs.SetInt("NextBank", 0);
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
}
