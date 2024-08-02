using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    bool isPlayerNearby = false;
    public TMP_Text interactText;
    public bool isWallet = true;

    private void Update()
    {
        // If player isn't nearby, don't do anything
        if (!isPlayerNearby)
        {
            return;
        }

        // If player presses E or F key
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            // Take the money
            if (!isWallet)
            {
                GameManager.instance.foundMoney = true;
                GameManager.instance.tasks[3].isOn = true;
                GameManager.instance.tasks[4].gameObject.SetActive(true);
                interactText.text = "";
                float money = PlayerPrefs.GetFloat("Money") + 1000;
                GameManager.instance.moneyText.text = "$" + money;
                PlayerPrefs.SetFloat("Money", money);
                GameManager.instance.playerMovement.speed *= 0.5f;
                Destroy(gameObject);
            }
            else
            {
                interactText.text = "";
                float money = PlayerPrefs.GetFloat("Money") + Random.Range(10, 51);
                GameManager.instance.moneyText.text = "$" + money;
                PlayerPrefs.SetFloat("Money", money);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player is near the money
        if (other.tag == "Player")
        {
            isPlayerNearby = true;
            if (!isWallet)
            {
                interactText.text = "Press E or F key to take the money.";
            }
            else
            {
                interactText.text = "Press E or F key to take the wallet.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNearby = false;
        interactText.text = "";
    }
}
