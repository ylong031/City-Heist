using UnityEngine;
using TMPro;
using System.Collections;

public class VaultDoor : MonoBehaviour
{
    bool isPlayerNearVaultDoor = false;

    bool isOpen = false;

    public TMP_Text interactText;

    private void Update()
    {
        // If player hasn't found the vault keycard / vault door has already been opened, don't do anything
        if (!GameManager.instance.foundVaultKeycard || isOpen)
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

    public IEnumerator OpenVaultDoor()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (GameManager.instance.isColourSquareTask)
        {
            foreach (var colourSquare in GameManager.instance.colourSquares)
            {
                if (colourSquare.color == Color.green)
                {
                    continue;
                }
                colourSquare.color = Color.green;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            GameManager.instance.currentCode.color = Color.green;
            GameManager.instance.currentCode.text = "SUCCESS";
            yield return new WaitForSeconds(1.5f);
        }

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
        //Destroy(gameObject);
        GetComponent<Animator>().enabled = true;
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
            // If player has found the vault keycard & jammed the CCTV
            if (GameManager.instance.foundVaultKeycard && GameManager.instance.jammedCCTV)
            {
                isPlayerNearVaultDoor = true;

                // Display interact text
                interactText.text = "Press E or F key to use the vault keycard and crack open the vault door.";
            }
            else if (!GameManager.instance.jammedCCTV)
            {
                // Display interact text
                interactText.text = "The CCTVs around the vault are still active.";
            }
            else if(!GameManager.instance.foundVaultKeycard)
            {
                // Display interact text
                interactText.text = "You need the vault keycard to crack open the vault door.";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNearVaultDoor = false;
        interactText.text = "";
    }
}
