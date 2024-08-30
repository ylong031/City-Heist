using UnityEngine;
using TMPro;
using System.Collections;

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
    public TMP_Text text;

    public GameObject itemDrop;

    void Start()
    {
        StartCoroutine(ChangeBankManagerDialogue());
    }

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
                if (!GameManager.instance.takenHostage && PlayerPrefs.GetInt("NextBank", 0) == 0)
                {
                    nameText.text = "";
                    dialogueText.text = "You have just taken a hostage! You now have more time for your heist as the police can't act carelessly with a hostage's life at stake!";
                    text.enabled = false;
                    GameManager.instance.takenHostage = true;
                }
                else
                {
                    dialoguePanel.SetActive(false);
                }
                GameManager.instance.remainingTime += GameManager.instance.takeHostageReward;
                //StartCoroutine(GameManager.instance.ChangeRemainingTime(GameManager.instance.takeHostageReward));
                personLyingDown.SetActive(true);
                gameObject.SetActive(false);
                if (name == "Bank Manager" && !GameManager.instance.isColourSquareTask)
                {
                    itemDrop.SetActive(true);
                }
                else if (name == "Security Guard" && itemDrop != null)
                {
                    itemDrop.SetActive(true);
                }
            }
        }
    }

    IEnumerator ChangeBankManagerDialogue()
    {
        yield return new WaitForSeconds(1f);
        if (name == "Bank Manager")
        {
            if (GameManager.instance.vaultCodeMemo != null && GameManager.instance.vaultCodeMemo.activeSelf)
            {
                dialogue = "I don't remember what the vault code is, but I swear a memo of it is in the safe at my office! Okay I told you already, please......just don't kill me......";
            }
            else
            {
                if (GameManager.instance.isColourSquareTask)
                {
                    dialogue = "Please......just don't kill me......";
                }
                else
                {
                    dialogue = "Here! This memo has the code to the vault! Please......just don't kill me......";
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
            text.enabled = true;
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
            if (!GameManager.instance.killedCivilian)
            {
                nameText.text = "";
                dialogueText.text = "You have just killed a civilian! You now have less time for your heist as the police will now do everything in their power to arrest you!";
                dialoguePanel.SetActive(true);
                text.enabled = false;
                GameManager.instance.killedCivilian = true;
            }
            else
            {
                dialoguePanel.SetActive(false);
                text.enabled = true;
            }
            GameManager.instance.remainingTime -= GameManager.instance.killHostagePenalty;
            //StartCoroutine(GameManager.instance.ChangeRemainingTime(-GameManager.instance.killHostagePenalty));
            if (itemDrop != null)
            {
                if (name == "Bank Manager" && GameManager.instance.isColourSquareTask)
                {
                    itemDrop.SetActive(false);
                }
                else
                {
                    if (itemDrop != null)
                    {
                        itemDrop.SetActive(true);
                    }
                }
            }
            personLyingDown.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
