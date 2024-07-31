using UnityEngine;
using TMPro;

public class CCTVConsole : MonoBehaviour
{
    bool isPlayerNearConsole = false;

    bool isHacked = false;

    public TMP_Text interactText;

    private void Update()
    {
        // If player isn't near CCTV console, don't do anything
        if (!isPlayerNearConsole)
        {
            return;
        }

        // If player presses E or F key
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            // Hack / Jam CCTV Console
            Cursor.lockState = CursorLockMode.None;
            GameManager.instance.playerMovement.enabled = false;
            GameManager.instance.cctvPanel.SetActive(true);
        }
    }

    public void HackJamCCTV()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.playerMovement.enabled = true;
        GameManager.instance.cctvPanel.SetActive(false);

        isHacked = true;
        GameManager.instance.hackedCCTV = true;
        GameManager.instance.tasks[0].isOn = true;
        GameManager.instance.tasks[1].gameObject.SetActive(true);
        interactText.text = "You successfuly hacked / jammed the CCTV! Police will now arrive 30s later!";
        GameManager.instance.remainingTime += 30f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If CCTV console is already hacked / jammed, don't do anything
        if (isHacked & !other.name.Contains("Bullet"))
        {
            interactText.text = "You already hacked / jammed the CCTV!";
            return;
        }

        // If player is near console
        if(other.tag == "Player")
        {
            isPlayerNearConsole = true;

            // Display interact text
            interactText.text = "Press E or F key to hack / jam the CCTV.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNearConsole = false;
        interactText.text = "";
    }
}
