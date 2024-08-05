using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class CCTVConsole : MonoBehaviour
{
    bool isPlayerNearConsole = false;

    bool isJammed = false;

    public TMP_Text interactText;

    public GameObject computerVirus;
    public float computerVirusAnimationTime;
    public GameObject tintPanel;
    [HideInInspector]
    public bool isBeingJammed = false;

    private void Update()
    {
        // If player isn't near CCTV console / CCTV has already been jammed, don't do anything
        if (!isPlayerNearConsole || isJammed)
        {
            return;
        }

        // If player presses E or F key
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            // Jam CCTV Console
            Cursor.lockState = CursorLockMode.None;
            GameManager.instance.thirdPersonCamera.enabled = false;
            GameManager.instance.playerMovement.enabled = false;
            GameManager.instance.cctvPanel.SetActive(true);
        }
    }

    public IEnumerator JamCCTV()
    {
        Cursor.lockState = CursorLockMode.Locked;

        computerVirus.SetActive(true);
        yield return new WaitForSeconds(computerVirusAnimationTime);

        yield return new WaitForSeconds(0.25f);

        tintPanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        tintPanel.SetActive(false);

        yield return new WaitForSeconds(1f);

        tintPanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        tintPanel.SetActive(false);

        yield return new WaitForSeconds(1f);

        tintPanel.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        tintPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        GameManager.instance.thirdPersonCamera.enabled = true;
        GameManager.instance.playerMovement.enabled = true;
        GameManager.instance.cctvPanel.SetActive(false);

        isJammed = true;
        GameManager.instance.jammedCCTV = true;
        GameManager.instance.tasks[0].isOn = true;
        GameManager.instance.tasks[1].gameObject.SetActive(true);
        if (GameManager.instance.tasks[1].isOn)
        {
            GameManager.instance.tasks[2].gameObject.SetActive(true);
        }
        interactText.text = "You successfuly jammed the CCTV! Police will now arrive " + GameManager.instance.jamCCTVReward + "s later!";
        //GameManager.instance.remainingTime += GameManager.instance.jamCCTVReward;
        StartCoroutine(GameManager.instance.ChangeRemainingTime(GameManager.instance.jamCCTVReward));
    }

    private void OnTriggerEnter(Collider other)
    {
        // If CCTV console is already jammed, don't do anything
        if (isJammed & !other.name.Contains("Bullet"))
        {
            interactText.text = "You already jammed the CCTV!";
            return;
        }

        // If player is near console
        if(other.tag == "Player")
        {
            isPlayerNearConsole = true;

            // Display interact text
            interactText.text = "Press E or F key to jam the CCTV.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerNearConsole = false;
        interactText.text = "";
    }
}
