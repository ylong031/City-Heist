using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    //add reference to the AudioSource and AudioClip
    public AudioSource buttonAudioSource;
    public AudioClip buttonSound;

    //initialize the audio source component
    private void Start()
    {
        buttonAudioSource = gameObject.AddComponent<AudioSource>();
    }

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
                        PlayerPrefs.SetFloat("Time", GameManager.instance.remainingTime);
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
                        PlayerPrefs.SetFloat("Time", GameManager.instance.remainingTime);
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
                GameManager.instance.escapePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                GameManager.instance.thirdPersonCamera.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.instance.escapePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.thirdPersonCamera.enabled = true;
    }

    public void Escape()
    {
        //play the button sound when player choose to escape
        buttonAudioSource.PlayOneShot(buttonSound);

        // Bank 2
        if (SceneManager.GetActiveScene().name == "Bank 2")
        {
            // 1st Bank - Bank 2
            if (PlayerPrefs.GetInt("NextBank", 0) == 0)
            {
                // Retreat
                PlayerPrefs.SetInt("NextBank", 1);
                PlayerPrefs.SetFloat("Time", GameManager.instance.remainingTime);
                PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
            }
            // 2nd (Final) Bank - Bank 2
            else
            {
                // End
                //SceneManager.LoadScene("End Scene");
                PlayerPrefs.SetInt("NextBank", 0);
                PlayerPrefs.SetFloat("Time", GameManager.instance.remainingTime);
                PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
            }

        }
        // Bank 1
        else
        {
            // 1st Bank - Bank 1
            if (PlayerPrefs.GetInt("NextBank", 0) == 0)
            {
                // Retreat
                PlayerPrefs.SetInt("NextBank", 2);
                PlayerPrefs.SetFloat("Time", GameManager.instance.remainingTime);
                PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
            }
            // 2nd (Final) Bank - Bank 1
            else
            {
                // End
                //SceneManager.LoadScene("CityScene");
                PlayerPrefs.SetInt("NextBank", 0);
                PlayerPrefs.SetFloat("Time", GameManager.instance.remainingTime);
                PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
            }
        }
    }
}
