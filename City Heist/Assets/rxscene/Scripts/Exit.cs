using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public TMP_Text interactText;
    public string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        // If player is near the exit
        if (other.tag == "Player")
        {
            if (GameManager.instance.foundMoney)
            {
                // Win
                //SceneManager.LoadScene(nextScene);
                PlayerPrefs.SetFloat("Money", GameManager.instance.money);
                StartCoroutine(SceneTransition.instance.TransitionToScene(nextScene));
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
