using TMPro;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject youWinPanel;

    public TMP_Text interactText;

    private void OnTriggerEnter(Collider other)
    {
        // If player is near the exit
        if (other.tag == "Player")
        {
            if (GameManager.instance.foundMoney)
            {
                youWinPanel.SetActive(true);
                GameManager.instance.playerMovement.enabled = false;
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
