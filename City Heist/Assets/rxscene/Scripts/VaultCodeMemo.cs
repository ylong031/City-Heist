using TMPro;
using UnityEngine;

public class VaultCodeMemo : MonoBehaviour
{

    bool isPlayerNearby = false;
    public TMP_Text interactText;
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public TMP_Text text;

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
            // Display vault code memo text
            GetComponent<Renderer>().enabled = false;
            GetComponentInChildren<TMP_Text>().enabled = false;
            interactText.text = "";
            dialoguePanel.SetActive(true);
            text.enabled = false;
            nameText.text = "";
            //dialogueText.text = "The vault code for today is: " + GameManager.instance.vaultCode;
            dialogueText.text = GameManager.instance.vaultCodeMemoText;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player is near the vault code memo
        if (other.tag == "Player")
        {
            isPlayerNearby = true;
            interactText.text = "Press E or F key to read memo.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Renderer>().enabled = true;
        GetComponentInChildren<TMP_Text>().enabled = true;
        isPlayerNearby = false;
        interactText.text = "";
        dialoguePanel.SetActive(false);
        text.enabled = true;
    }
}
