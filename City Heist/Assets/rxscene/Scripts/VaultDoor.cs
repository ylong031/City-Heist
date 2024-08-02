using UnityEngine;
using TMPro;

public class VaultDoor : MonoBehaviour
{
    bool isPlayerNearVaultDoor = false;

    bool isOpen = false;

    public TMP_Text interactText;

    private void Update()
    {
        // If player hasn't found the vault keycard, don't do anything
        if (!GameManager.instance.foundVaultKeycard)
        {
            return;
        }

        // If player is near vault door
        if (isPlayerNearVaultDoor)
        {
            // If player presses E or F key
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
            {
                if (!GameManager.instance.isColourSquareTask)
                {
                    // Open bank vault door keypad
                    Cursor.lockState = CursorLockMode.None;
                    GameManager.instance.thirdPersonCamera.enabled = false;
                    GameManager.instance.playerMovement.enabled = false;
                    GameManager.instance.vaultKeypad.SetActive(true);
                }
                else
                {
                    // Open colour square panel
                    Cursor.lockState = CursorLockMode.None;
                    GameManager.instance.thirdPersonCamera.enabled = false;
                    GameManager.instance.playerMovement.enabled = false;
                    GameManager.instance.colourSquarePanel.SetActive(true);
                }
            }
        }
    }

    public void OpenVaultDoor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.thirdPersonCamera.enabled = true;
        GameManager.instance.playerMovement.enabled = true;
        if (!GameManager.instance.isColourSquareTask)
        {
            GameManager.instance.vaultKeypad.SetActive(false);
        }
        else
        {
            GameManager.instance.colourSquarePanel.SetActive(false);
        }

        isOpen = true;
        GameManager.instance.tasks[2].isOn = true;
        GameManager.instance.tasks[3].gameObject.SetActive(true);
        interactText.text = "";
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If vault door is already opened, don't do anything
        if (isOpen)
        {
            return;
        }

        // If player is near vault door
        if (other.tag == "Player")
        {
            // If player has found the vault keycard & hacked the CCTV
            if (GameManager.instance.foundVaultKeycard && GameManager.instance.hackedCCTV)
            {
                isPlayerNearVaultDoor = true;

                // Display interact text
                interactText.text = "Press E or F key to open vault door.";
            }
            else if (!GameManager.instance.hackedCCTV)
            {
                // Display interact text
                interactText.text = "The CCTVs around the vault are still active.";
            }
            else if(!GameManager.instance.foundVaultKeycard)
            {
                // Display interact text
                interactText.text = "You need the vault keycard.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNearVaultDoor = false;
        interactText.text = "";
    }
}
