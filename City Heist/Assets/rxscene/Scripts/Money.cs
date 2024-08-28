using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    bool isPlayerNearby = false;
    public TMP_Text interactText;
    public bool isWallet = true;
    public GameObject[] moneyObjs;
    public bool isMiniSafe = false;
    bool isPickedUp = false;

    private void Update()
    {
        // If player isn't nearby, don't do anything
        if (!isPlayerNearby || isPickedUp)
        {
            return;
        }

        // If player presses E or F key
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            // Take the money
            if (!isWallet)
            {
                if (isMiniSafe)
                {
                    // Mini safe money
                    interactText.text = "";
                    //float money = PlayerPrefs.GetFloat("Money") + GameManager.instance.vaultMoneyReward;
                    //GameManager.instance.moneyText.text = "$" + money;
                    //PlayerPrefs.SetFloat("Money", money);
                    GameManager.instance.money += GameManager.instance.vaultMoneyReward / 2;
                    GameManager.instance.moneyText.text = "$" + GameManager.instance.money.ToString();
                    //Destroy(gameObject);
                    foreach (var moneyObj in moneyObjs)
                    {
                        Destroy(moneyObj);
                    }
                }
                else
                {
                    // Vault money
                    GameManager.instance.foundMoney = true;
                    GameManager.instance.tasks[3].isOn = true;
                    GameManager.instance.tasks[4].gameObject.SetActive(true);
                    interactText.text = "";
                    //float money = PlayerPrefs.GetFloat("Money") + GameManager.instance.vaultMoneyReward;
                    //GameManager.instance.moneyText.text = "$" + money;
                    //PlayerPrefs.SetFloat("Money", money);
                    GameManager.instance.money += GameManager.instance.vaultMoneyReward;
                    GameManager.instance.moneyText.text = "$" + GameManager.instance.money.ToString();
                    GameManager.instance.playerMovement.speed *= 0.5f;
                    //Destroy(gameObject);
                    foreach (var moneyObj in moneyObjs)
                    {
                        Destroy(moneyObj);
                    }
                }
            }
            else
            {
                // Wallet Money
                interactText.text = "";
                //float money = PlayerPrefs.GetFloat("Money") + Random.Range(GameManager.instance.minWalletMoneyReward, GameManager.instance.maxWalletMoneyReward + 1);
                //GameManager.instance.moneyText.text = "$" + money;
                //PlayerPrefs.SetFloat("Money", money);
                GameManager.instance.money += Random.Range(GameManager.instance.minWalletMoneyReward, GameManager.instance.maxWalletMoneyReward + 1);
                GameManager.instance.moneyText.text = "$" + GameManager.instance.money.ToString();
                Destroy(gameObject);
            }
            isPickedUp = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player is near the money
        if (other.tag == "Player" && !isPickedUp)
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
