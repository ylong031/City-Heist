using UnityEngine;
using TMPro;

public class VaultKeycard : MonoBehaviour
{
    bool isPlayerNearby = false;
    public TMP_Text interactText;

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
            // Take the vault keycard
            GameManager.instance.foundVaultKeycard = true;
            GameManager.instance.tasks[1].isOn = true;
            if (GameManager.instance.tasks[1].gameObject.activeSelf)
            {
                GameManager.instance.tasks[2].gameObject.SetActive(true);
            }
            interactText.text = "";
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player is near the vault keycard
        if (other.tag == "Player")
        {
            isPlayerNearby = true;
            interactText.text = "Press E or F key to take vault keycard.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNearby = false;
        interactText.text = "";
    }
}
