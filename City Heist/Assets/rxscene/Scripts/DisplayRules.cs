using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayRules : MonoBehaviour
{
    string rules = "Welcome to City Heist!\nYou're a robber with a past filled with crimes that honed your skills for your biggest challenge yet.\nRobbing the city's most secure banks!\nRules: Drive to the bank on your map to start your robbery.\nAvoid hitting cars or you'll lose money!\nComplete robbing 2 banks in 10 minutes to complete your game.\nGood luck!";
    public GameObject leftClickContinueText;
    TMP_Text rulesText;
    string[] sentences;
    int index = 0;
    bool isRunning = false;
	
	//add reference to audiosource
    public AudioSource rulesAudioSource;
    public AudioSource startAudioSource;

    //create function to play the sound when clicking rules
    void RulesSound()
    {
        if (rulesAudioSource != null)
        {
            rulesAudioSource.Play();
        }
    }

    void Start()
    {
        rulesText = GetComponentInChildren<TMP_Text>();
        if (SceneManager.GetActiveScene().name == "Bank 1" || SceneManager.GetActiveScene().name == "Bank 2")
        {
            rules = "Your mission: Complete the tasks in the task list to escape with the loot!\nSteal as much money as possible to get a higher grade!\nRules: Shooting civilians will cost you time but will earn you extra money.\nTaking them hostage will delay the police but you won't get their money.\nChoose wisely!\nP.S: You may want to leave the staff alive till you leave the bank. They give you hints!";
        }
        sentences = rules.Split('\n');
        StartCoroutine(DisplayRulesOneSentenceAtATime());
    }

    void Update()
    {
        if (!isRunning && Input.GetMouseButtonDown(0))
        {
			//play the sound when user clicks the rule
            RulesSound();
			
            if (index == sentences.Length - 1)
            {
                if (startAudioSource != null)
                {
                    startAudioSource.Play();
                }

                if (SceneManager.GetActiveScene().name == "Bank 1" || SceneManager.GetActiveScene().name == "Bank 2")
                {
                    StartCoroutine(RulesPanelFadeOut());
                }
                else
                {
                    StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
                }
            }
            else
            {
                leftClickContinueText.SetActive(false);
                index++;
                StartCoroutine(DisplayRulesOneSentenceAtATime());
            }
        }
        else if (isRunning && Input.GetMouseButtonDown(0))
        {
			//play the sound when user clicks the rule
            RulesSound();
			
            StopAllCoroutines();
            for (int i = 0; i <= index; i++)
            {
                if (i == 0)
                {
                    rulesText.text = "";
                }
                rulesText.text += sentences[i] + "\n";
            }
            isRunning = false;
            leftClickContinueText.SetActive(true);
        }
    }

    IEnumerator DisplayRulesOneSentenceAtATime()
    {
        isRunning = true;
        foreach (var sentence in sentences[index])
        {
            rulesText.text += sentence;
            yield return new WaitForSeconds(0.01f);
        }
        rulesText.text += "\n";
        isRunning = false;
        leftClickContinueText.SetActive(true);
    }

    IEnumerator RulesPanelFadeOut()
    {
        GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
