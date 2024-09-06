using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string dialogue;

    public int health;

    Animator animator;
    bool isScared = false;
    public bool doesNotNoticePlayer = false;

    bool isPlayerNearby = false;
    bool isTalkedTo = false;

    // mng = Bank Manager; grd = Security Guard
    bool mngGrdIsTakenHostage = false;
    bool mngGrdIsShot = false;
    bool talkedToShotMngGrd = false;

    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public TMP_Text text;

    public GameObject itemDrop;

    HealthBar healthBar;
    GameObject bloodSpill;

    //setting up AudioClip for when NPCs gets hurt or dies
    public AudioClip NPCHurtSound;
    public AudioClip NPCDiedSound;
    public AudioSource NPCAudioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar = transform.GetChild(transform.childCount - 2).GetComponentInChildren<HealthBar>();
        bloodSpill = transform.GetChild(transform.childCount - 1).gameObject;
        healthBar.SetMaxHealth(health);

        //initialize the audiosource
        NPCAudioSource = GetComponent<AudioSource>();

        if (name.Contains("Girl"))
        {
            NPCAudioSource.pitch = 1.2f;
        }

        StartCoroutine(ChangeBankManagerDialogue());
    }

    private void Update()
    {
        if (!doesNotNoticePlayer)
        {
            //Debug.Log(Vector3.Distance(transform.position, GameManager.instance.playerMovement.transform.position));
            if (!isScared && Vector3.Distance(transform.position, GameManager.instance.playerMovement.transform.position) < 10f)
            {
                isScared = true;
                animator.SetTrigger("isScared");
            }

            if (!isTalkedTo && Vector3.Distance(transform.position, GameManager.instance.playerMovement.transform.position) < 10f)
            {
                // Rotate NPC towards player
                Vector3 targetPos = new Vector3(GameManager.instance.playerMovement.transform.position.x,
                                               transform.position.y,
                                               GameManager.instance.playerMovement.transform.position.z);
                transform.LookAt(targetPos, Vector3.up);
            }
        }

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
                GameManager.instance.takeHostageAudioSource.Play();

                isTalkedTo = true;
                if (!GameManager.instance.takenHostage/* && PlayerPrefs.GetInt("NextBank", 0) == 0*/)
                {
                    nameText.text = "";
                    dialogueText.text = "You have just taken a hostage! You now have more time for your heist as the police can't act carelessly with a hostage's life at stake!";
                    text.enabled = false;
                    //GameManager.instance.takenHostage = true;
                }
                else
                {
                    dialoguePanel.SetActive(false);
                }

                if (PlayerPrefs.GetInt("EnableMinimap", 1) == 1)
                {
                    GameManager.instance.takenHostage = true;
                }

                StartCoroutine(GameManager.instance.ChangeRemainingTime(GameManager.instance.takeHostageReward));
				healthBar.GetComponentInChildren<Image>().enabled = false;
                animator.SetTrigger("isTakenHostage");

                // Only the bank manager and the security guard can be talked to after being taken hostage
                if (name == "Bank Manager" || name == "Security Guard")
                {
                    //GetComponent<Renderer>().enabled = false;
                    mngGrdIsTakenHostage = true;
                }
                else
                {
					//GetComponent<Renderer>().enabled = false;
					enabled = false;					
                }
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
        // Talk to Bank Manager / Security Guard hostage
        if (mngGrdIsTakenHostage && other.tag == "Player")
        {
            if (name == "Bank Manager")
            {
                // Display dialogue panel and text
                dialoguePanel.SetActive(true);
                nameText.text = name;
                if (dialogue.Contains("office"))
                {
                    dialogueText.text = "The memo containing the code to the vault is in the safe at my office, just take it and leave!";
                }
                else if (dialogue.Contains("Here!"))
                {
                    dialogueText.text = "The memo containing the code to the vault is right there! Just take it and leave me be!";
                }
                else
                {
                    dialogueText.text = "Please......I don't want to die......";
                }
                text.enabled = false;
            }
            else if (name == "Security Guard")
            {
                // Display dialogue panel and text
                dialoguePanel.SetActive(true);
                nameText.text = name;
                if (dialogue.Contains("office"))
                {
                    dialogueText.text = "I already told you! I swear I put the vault keycard somewhere in the bank manager's office! Just go......";
                }
                else if (dialogue.Contains("security"))
                {
                    dialogueText.text = "I already told you! I swear I put the vault keycard somewhere in the security room! Just go away......";
                }
                else
                {
                    dialogueText.text = "Look! The vault keycard is just right there! Just take it and go away, please......";
                }
                text.enabled = false;
            }
        }
        // Talk to shot Bank Manager / Security Guard
        else if (mngGrdIsShot && !talkedToShotMngGrd && other.tag == "Player")
        {
            if (name == "Bank Manager")
            {
                // Display dialogue panel and text
                dialoguePanel.SetActive(true);
                nameText.text = name;
                if (dialogue.Contains("office"))
                {
                    dialogueText.text = "I already told you the memo containing the code to the vault is in the safe at my office, why did you shoot me anyways......";
                }
                else if (dialogue.Contains("Here!"))
                {
                    dialogueText.text = "The memo containing the code to the vault is just right there, why did you shoot me anyways......";
                }
                else
                {
                    dialogueText.text = "Kids......Forgive daddy......Take care of mommy for me......";
                }
                text.enabled = false;
            }
            else if (name == "Security Guard")
            {
                // Display dialogue panel and text
                dialoguePanel.SetActive(true);
                nameText.text = name;
                if (dialogue.Contains("office"))
                {
                    dialogueText.text = "I shouldn't have put the vault keycard on the table in the bank manager's office......Maybe that way I could have lived......";
                }
                else if (dialogue.Contains("security"))
                {
                    dialogueText.text = "Maybe I shouldn't have put the vault keycard on the table in the security room......That way I could have lived......";
                }
                else
                {
                    dialogueText.text = "The vault keycard is right there, but just know that you cannot turn back now......The police will definitely arrest you......";
                }
                text.enabled = false;
            }
            talkedToShotMngGrd = true;
        }

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
        if (talkedToShotMngGrd && other.tag == "Player")
        {
            // Spawn Blood Spill
            bloodSpill.gameObject.SetActive(true);
        }

        if (other.tag == "Player")
        {
            isPlayerNearby = false;
            dialoguePanel.SetActive(false);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (mngGrdIsTakenHostage || mngGrdIsShot || isTalkedTo)
        {
            return;
        }

        health -= dmg;

        //play the sound when NPCs gets hurt
        if (NPCAudioSource != null && NPCHurtSound != null)
        {
            NPCAudioSource.PlayOneShot(NPCHurtSound);
        }

        if (name == "Bank Manager" || name == "Security Guard")
        {
            StartCoroutine(healthBar.SetHealth(dmg));
        }
        else
        {
		    StartCoroutine(healthBar.SetHealth(dmg));
        }
        if(health <= 0)
        {
            if (name != "Bank Manager" && name != "Security Guard")
            {
                // Spawn Blood Spill
                bloodSpill.gameObject.SetActive(true);
            }

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

            StartCoroutine(GameManager.instance.ChangeRemainingTime(-GameManager.instance.killHostagePenalty));

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

            animator.SetTrigger("isDead");

            //play the sound when NPC dead
            if (NPCAudioSource != null && NPCDiedSound != null)
            {
                NPCAudioSource.PlayOneShot(NPCDiedSound);
            }

            // Only the bank manager and the security guard can be talked to ONCE after being shot
            if (name == "Bank Manager" || name == "Security Guard")
            {
                //GetComponent<Renderer>().enabled = false;
                mngGrdIsShot = true;
            }
            else
            {
				//GetComponent<Renderer>().enabled = false;
				enabled = false;
            }
        }
    }
}
