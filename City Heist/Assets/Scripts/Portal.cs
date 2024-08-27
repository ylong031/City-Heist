using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] string sceneName;
    public Misc misc;
    void Start()
    {
        if (PlayerPrefs.GetInt("NextBank", 0) == 2 && sceneName == "Bank 1")
        {
            gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("NextBank", 0) == 1 && sceneName == "Bank 2")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerPrefs.SetFloat("Time", misc.remainingTime);
            //SceneManager.LoadScene(sceneName);
            StartCoroutine(SceneTransition.instance.TransitionToScene(sceneName));
        }
    }
}
