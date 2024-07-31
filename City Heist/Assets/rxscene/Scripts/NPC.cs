using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public string dialogue;

    public int health;
    public GameObject personLyingDown;

    bool isPlayerNearby = false;
    bool isTalkedTo = false;

    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject vaultKeycard;

    private void Update()
    {
        // If player already talked to, don't do anything
        if (isTalkedTo)
        {
            return;
        }
        
        // If player is nearby
        if (isPlayerNearby)
        {
            // If player presses E or F key
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
            {
                // Take hostage
                isTalkedTo = true;
                GameManager.instance.remainingTime += 10f;
                dialoguePanel.SetActive(false);
                personLyingDown.SetActive(true);
                gameObject.SetActive(false);
                if (name == "Bank Manager")
                {
                    GameManager.instance.foundVaultKeycard = true;
                    GameManager.instance.tasks[1].isOn = true;
                    GameManager.instance.tasks[2].gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player already talked to, don't do anything
        if (isTalkedTo)
        {
            return;
        }

        // If player is nearby
        if (other.tag == "Player")
        {
            isPlayerNearby = true;

            // Display dialogue panel and text
            dialoguePanel.SetActive(true);
            nameText.text = name;
            dialogueText.text = dialogue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNearby = false;
        dialoguePanel.SetActive(false);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            personLyingDown.GetComponent<Renderer>().material.color = Color.red;
            isTalkedTo = true;
            GameManager.instance.remainingTime -= 15f;
            if (name == "Bank Manager")
            {
                vaultKeycard.SetActive(true);
            }
            dialoguePanel.SetActive(false);
            personLyingDown.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
