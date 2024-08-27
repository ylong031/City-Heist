using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public TMP_Text interactText;

    private void OnTriggerEnter(Collider other)
    {
        // If player is near the exit
        if (other.tag == "Player")
        {
            if (GameManager.instance.foundMoney)
            {
                // Win
                //SceneManager.LoadScene(nextScene);

                //PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                //StartCoroutine(SceneTransition.instance.TransitionToScene(nextScene));

                // Bank 2
                if (SceneManager.GetActiveScene().name == "Bank 2")
                {
                    // 2nd (Final) Bank - Bank 2
                    if (PlayerPrefs.GetInt("NextBank", 0) == 2)
                    {
                        PlayerPrefs.SetInt("NextBank", 0);
                        PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                        StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
                    }
                    // 1st Bank - Bank 2
                    else
                    {
                        PlayerPrefs.SetFloat("Time", GameManager.instance.remainingTime);
                        PlayerPrefs.SetInt("NextBank", 1);
                        PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                        StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
                    }

                }
                // Bank 1
                else
                {
                    // 2nd (Final) Bank - Bank 1
                    if (PlayerPrefs.GetInt("NextBank", 0) == 1)
                    {
                        PlayerPrefs.SetInt("NextBank", 0);
                        PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                        StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
                    }
                    // 1st Bank - Bank 1
                    else
                    {
                        PlayerPrefs.SetFloat("Time", GameManager.instance.remainingTime);
                        PlayerPrefs.SetInt("NextBank", 2);
                        PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                        StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
                    }
                }
            }
            else
            {
                interactText.text = "You have not cracked the vault and stolen the money!";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactText.text = "";
    }
}
