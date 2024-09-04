using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Game Manager Singleton
    public static GameManager instance;

    [HideInInspector]
    public bool foundVaultKeycard = false;
    [HideInInspector]
    public bool foundMoney = false;
    [HideInInspector]
    public bool jammedCCTV = false;
    [HideInInspector]
    public bool takenHostage = false;
    [HideInInspector]
    public bool killedCivilian = false;

    // Countdown Timer
    public float remainingTime;
    public bool isTimerRunning = false;
    public TMP_Text countdownTimerText;
    public TMP_Text countdownTimerChangesText;

    public PlayerMovement playerMovement;
    public CinemachineFreeLook thirdPersonCamera;

    public GameObject instructionsPanel;

    // Vault Door Task A (Input Vault Code)
    [HideInInspector]
    public string vaultCode;
    public GameObject vaultKeypad;
    public TMP_Text currentCode;
    public VaultDoor vaultDoor;

    // Vault Door Task A / B
    public bool isColourSquareTask;

    // Vault Door Task B (Colour Squares Task)
    [HideInInspector]
    public int currentIndex = 1;
    public Image[] colourSquares;
    public GameObject colourSquarePanel;
    public int finalIndex;
    // Randomize Colour Squares Task Correct Path
    public RandomizeColourSquares randomizer;

    // CCTV Console Task
    public GameObject cctvPanel;
    public Image[] wiresToRotate;
    public CCTVConsole cctvConsole;

    // Task list
    public Toggle[] tasks;

    // Carry over money from city scene
    public TMP_Text moneyText;
    [HideInInspector]
    public float money;

    // Rewards (Time)
    public float jamCCTVReward;
    public float takeHostageReward;

    // Penalties (Time)
    public float killHostagePenalty;
    public float vaultDoorPenalty;
    public float colourSquareTaskPenalty;
    public float cctvConsolePenalty;

    // Rewards (Money)
    public int minWalletMoneyReward;
    public int maxWalletMoneyReward;
    public float vaultMoneyReward;
    public float miniSafeMoneyReward;

    [HideInInspector]
    public string vaultCodeMemoText;
    public GameObject vaultCodeMemo;
    public GameObject[] vaultKeycards;
    public NPC securityGuard;

    public GameObject escapePanel;

    public GameObject fracturedGlass;
    public GameObject wideFracturedGlass;

    public GameObject rulesPanel;
    public GameObject staticRulesPanel;

    public TMP_Text camSensText;
    public TMP_Text playerMoveSpeedText;
    public Slider camSensSlider;
    public Slider playerMoveSpeedSlider;

    public int currentLevel = 1;

    public TMP_Dropdown qualityDropdown;

    //add reference to the AudioSource and AudioClip
    public AudioSource buttonAudioSource;
    public AudioClip buttonSound;

    void Awake()
    {
        // Game Manager Singleton
        instance = this;
    }

    private void Start()
    {
        if (qualityDropdown != null)
        {
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
        }
        camSensSlider.value = PlayerPrefs.GetFloat("CameraSensitivity", 225f);
        camSensText.text = PlayerPrefs.GetFloat("CameraSensitivity", 225f).ToString();
        playerMoveSpeedSlider.value = PlayerPrefs.GetFloat("PlayerMoveSpeed", 8f);
        playerMoveSpeedText.text = PlayerPrefs.GetFloat("PlayerMoveSpeed", 8f).ToString();

        if (PlayerPrefs.GetInt("EnableMinimap", 1) == 0)
        {
            jamCCTVReward = 25f;
            takeHostageReward = 3f;
            killHostagePenalty = 15f;
            vaultDoorPenalty = 12f;
            colourSquareTaskPenalty = 12f;
            cctvConsolePenalty = 12f;
            minWalletMoneyReward = 40;
            maxWalletMoneyReward = 200;
            vaultMoneyReward = 2000f;
            miniSafeMoneyReward = 1000f;
        }

        if (PlayerPrefs.GetInt("NextBank", 0) != 0)
        {
            killedCivilian = true;
        }

        Cursor.lockState = CursorLockMode.None;

        // Carry over money from city scene
        //moneyText.text = "$" + PlayerPrefs.GetFloat("Money").ToString();
        money = PlayerPrefs.GetFloat("Money");
        moneyText.text = "$" + money.ToString();

        // Carry over remaining time from city scene
        remainingTime = PlayerPrefs.GetFloat("Time", 1000f);
        DisplayRemainingTime(remainingTime);

        // Randomize wires rotation
        foreach (var wire in wiresToRotate)
        {
            var rand = Random.Range(0, 4);
            if (rand == 0)
            {
                if (wire.transform.eulerAngles.z == 0f)
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                }
                else
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
            }
            else if (rand == 1)
            {
                if (wire.transform.eulerAngles.z == 90f)
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                }
                else
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                }
            }
            else if (rand == 2)
            {
                if (wire.transform.eulerAngles.z == 180f)
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                }
                else
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                }
            }
            else if (rand == 3)
            {
                if (wire.transform.eulerAngles.z == 270f)
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
                else
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                }
            }
        }

        // Randomizes which bank vault uses vault code and colour square task
        if (PlayerPrefs.GetInt("isColourSquareTask", -1) == -1)
        {
            var rand2 = Random.Range(0, 2);
            if (rand2 == 0)
            {
                isColourSquareTask = false;
                PlayerPrefs.SetInt("isColourSquareTask", 0);
            }
            else if (rand2 == 1)
            {
                isColourSquareTask = true;
                PlayerPrefs.SetInt("isColourSquareTask", 1);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("isColourSquareTask", -1) == 0)
            {
                isColourSquareTask = true;
                PlayerPrefs.SetInt("isColourSquareTask", -1);
            }
            else if (PlayerPrefs.GetInt("isColourSquareTask", -1) == 1)
            {
                isColourSquareTask = false;
                PlayerPrefs.SetInt("isColourSquareTask", -1);
            }
        }

        if (!isColourSquareTask)
        {
            // Spawn vault code memo inside bank manager's room safe (Bank 2 only)
            if(vaultCodeMemo != null)
            {
                vaultCodeMemo.SetActive(true);
            }
            // Generate 4-digit bank vault code
            GenerateVaultCode();
        }

        // Spawn vault keycard on security guard / inside the bank
        var rand3 = Random.Range(0, 2);
        if (rand3 == 0)
        {
            Destroy(vaultKeycards[0]);
            securityGuard.dialogue = "Here's the keycard to the vault! Take it! Just take it and don't hurt me......";
        }
        if (rand3 == 1)
        {
            Destroy(vaultKeycards[1]);
            if (SceneManager.GetActiveScene().name == "Bank 2")
            {
                securityGuard.dialogue = "I swear I put the vault keycard somewhere in the bank manager's office! I told you what I know already, just don't hurt me......";
            }
            else
            {
                securityGuard.dialogue = "I swear I put the vault keycard somewhere in the security room! I told you what I know already, just don't hurt me......";
            }
        }

        // 2nd bank
        if (PlayerPrefs.GetInt("NextBank", 0) != 0)
        {
            CloseInstructionsPanel();
        }
        // 1st bank
        else
        {
            rulesPanel.SetActive(true);
        }
    }

    //create function to play the sound
    private void PlayButtonSound()
    {
        if (buttonAudioSource != null && buttonSound != null)
        {
            buttonAudioSource.PlayOneShot(buttonSound);
        }
    }

    void Update()
    {
        // Press T to open / close task list
        if (Input.GetKeyDown(KeyCode.T))
        {
            tasks[0].transform.parent.gameObject.SetActive(!tasks[0].transform.parent.gameObject.activeSelf);
        }

        // Countdown Timer
        if (isTimerRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                DisplayRemainingTime(remainingTime);
            }
            else
            {
                Debug.Log("Time has run out!");
                remainingTime = 0;
                isTimerRunning = false;

                // If the player has taken at least one hostage
                if (takenHostage)
                {
                    //// Bank 2
                    //if (SceneManager.GetActiveScene().name == "Bank 2")
                    //{
                    //    // 1st Bank - Bank 2
                    //    if (PlayerPrefs.GetInt("NextBank", 0) == 0)
                    //    {
                    //        // Retreat
                    //        PlayerPrefs.SetInt("NextBank", 1);
                    //        PlayerPrefs.SetFloat("Time", remainingTime);
                    //        PlayerPrefs.SetFloat("Money", money);
                    //        StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
                    //    }
                    //    // 2nd (Final) Bank - Bank 2
                    //    else
                    //    {
                    //        // End
                    //        //SceneManager.LoadScene("End Scene");
                    //        PlayerPrefs.SetInt("NextBank", 0);
                    //        PlayerPrefs.SetFloat("Money", money);
                    //        StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
                    //    }

                    //}
                    //// Bank 1
                    //else
                    //{
                    //    // 1st Bank - Bank 1
                    //    if (PlayerPrefs.GetInt("NextBank", 0) == 0)
                    //    {
                    //        // Retreat
                    //        PlayerPrefs.SetInt("NextBank", 2);
                    //        PlayerPrefs.SetFloat("Time", remainingTime);
                    //        PlayerPrefs.SetFloat("Money", money);
                    //        StartCoroutine(SceneTransition.instance.TransitionToScene("CityScene"));
                    //    }
                    //    // 2nd (Final) Bank - Bank 1
                    //    else
                    //    {
                    //        // End
                    //        //SceneManager.LoadScene("CityScene");
                    //        PlayerPrefs.SetInt("NextBank", 0);
                    //        PlayerPrefs.SetFloat("Money", money);
                    //        StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
                    //    }
                    //}
                    
                    // Lose (Some money gained)
                    PlayerPrefs.SetInt("NextBank", 0);
                    PlayerPrefs.SetFloat("Time", remainingTime);
                    PlayerPrefs.SetFloat("Money", money);
                    StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
                }
                // If the player has not taken any hostages
                else
                {
                    // Lose (No money gained)
                    //SceneManager.LoadScene("End Scene");
                    //PlayerPrefs.SetInt("NextBank", 0);
                    //PlayerPrefs.SetFloat("Time", remainingTime);
                    //PlayerPrefs.SetFloat("Money", 0);
                    PlayerPrefs.SetInt("GameLost", 1);
                    StartCoroutine(SceneTransition.instance.TransitionToScene("End Scene"));
                }
            }
        }

        // CCTV Panel
        if (cctvPanel.activeSelf && !cctvConsole.isBeingJammed)
        {
            foreach (var wire in wiresToRotate)
            {
                if (!wire.GetComponent<WiresToRotate>().isInCorrectRot)
                {
                    return;
                }
            }
            cctvConsole.isBeingJammed = true;
            StartCoroutine(cctvConsole.JamCCTV());
        }
    }

    void DisplayRemainingTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GenerateVaultCode()
    {
        // Generate a random 4-digit bank vault code
        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int num3 = Random.Range(0, 10);
        int num4 = Random.Range(0, 10);
        vaultCode = num1.ToString() + num2.ToString() + num3.ToString() + num4.ToString();
        //Debug.Log("The code to the vault is: " + vaultCode);
        GenerateVaultCodeMemoText(num1, num2, num3, num4);

    }

    void GenerateVaultCodeMemoText(int digit1, int digit2, int digit3, int digit4)
    {
        var rand = Random.Range(0, 4);
        if (rand == 0)
        {
            //vaultCodeMemoText = string.Format("The vault code for today is: {0}ABC\n{0} + A = {1}\nA + B = {2}\nB + C = {3}\n", digit1, digit1 + digit2, digit2 + digit3, digit3 + digit4);
            vaultCodeMemoText = string.Format("The vault code for today is: ABCD\nA = {0}\nA + B = {1}\nB + C = {2}\nC + D = {3}", digit1, digit1 + digit2, digit2 + digit3, digit3 + digit4);
        }
        else if (rand == 1)
        {
            //vaultCodeMemoText = string.Format("The vault code for today is: A{0}BC\nA + {0} = {1}\nA + B = {2}\nB + C = {3}\n", digit2, digit1 + digit2, digit1 + digit3, digit3 + digit4);
            vaultCodeMemoText = string.Format("The vault code for today is: ABCD\nA + B = {1}\nB = {0}\nB + C = {2}\nC + D = {3}", digit2, digit1 + digit2, digit2 + digit3, digit3 + digit4);
        }
        else if (rand == 2)
        {
            //vaultCodeMemoText = string.Format("The vault code for today is: AB{0}C\nA + B = {1}\nB + {0} = {2}\nB + C = {3}\n", digit3, digit1 + digit2, digit2 + digit3, digit2 + digit4);
            vaultCodeMemoText = string.Format("The vault code for today is: ABCD\nA + B = {1}\nB + C = {2}\nC = {0}\nC + D = {3}", digit3, digit1 + digit2, digit2 + digit3, digit3 + digit4);
        }
        else if (rand == 3)
        {
            //vaultCodeMemoText = string.Format("The vault code for today is: ABC{0}\nA + B = {1}\nB + C = {2}\nC + {0} = {3}\n", digit4, digit1 + digit2, digit2 + digit3, digit3 + digit4);
            vaultCodeMemoText = string.Format("The vault code for today is: ABCD\nA + B = {1}\nB + C = {2}\nC + D = {3}\nD = {0}", digit4, digit1 + digit2, digit2 + digit3, digit3 + digit4);
        }
        //Debug.Log(vaultCodeMemoText);
    }

    public void InputVaultCode(int num)
    {
        if (currentCode.text.Length < 4)
        {
            currentCode.text += num;
        }
    }

    public void ConfirmVaultCode()
    {
        if (currentCode.text.Length == 4)
        {
            if (currentCode.text == vaultCode)
            {
                StartCoroutine(vaultDoor.OpenVaultDoor());
            }
            else
            {
                //remainingTime -= vaultDoorPenalty;
                StartCoroutine(ChangeRemainingTime(-vaultDoorPenalty));
                StartCoroutine(IncorrectCode());
            }
        }
    }

    IEnumerator IncorrectCode()
    {
        currentCode.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        currentCode.color = Color.white;
        currentCode.text = "";
    }

    public void DeleteVaultCode()
    {
        if (currentCode.text.Length > 0)
        {
            currentCode.text = currentCode.text.Remove(currentCode.text.Length - 1);
        }
    }

    public void CloseKeypad()
    {
        //remainingTime -= vaultDoorPenalty;
        StartCoroutine(ChangeRemainingTime(-vaultDoorPenalty));
        Cursor.lockState = CursorLockMode.Locked;
        currentCode.text = "";
        vaultKeypad.SetActive(false);
        thirdPersonCamera.enabled = true;
        playerMovement.enabled = true;
    }

    public void CloseCCTVPanel()
    {
        // Randomize wires rotation
        foreach (var wire in wiresToRotate)
        {
            wire.GetComponent<WiresToRotate>().isInCorrectRot = false;
            wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, wire.GetComponent<WiresToRotate>().correctRot));

            var rand = Random.Range(0, 4);
            if (rand == 0)
            {
                if (wire.transform.eulerAngles.z == 0f)
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                }
                else
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
            }
            else if (rand == 1)
            {
                if (wire.transform.eulerAngles.z == 90f)
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                }
                else
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                }
            }
            else if (rand == 2)
            {
                if (wire.transform.eulerAngles.z == 180f)
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                }
                else
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                }
            }
            else if (rand == 3)
            {
                if (wire.transform.eulerAngles.z == 270f)
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
                else
                {
                    wire.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                }
            }
        }

        //remainingTime -= cctvConsolePenalty;
        StartCoroutine(ChangeRemainingTime(-cctvConsolePenalty));
        Cursor.lockState = CursorLockMode.Locked;
        cctvPanel.SetActive(false);
        thirdPersonCamera.enabled = true;
        playerMovement.enabled = true;
    }

    public void CloseColourSquarePanel()
    {
        // Reset Colour Squares
        currentIndex = 1;
        StartCoroutine(ChangeRemainingTime(-colourSquareTaskPenalty));
        foreach (Image colourSquare in colourSquares)
        {
            colourSquare.GetComponent<Image>().color = Color.white;
        }

        Cursor.lockState = CursorLockMode.Locked;
        colourSquarePanel.SetActive(false);
        thirdPersonCamera.enabled = true;
        playerMovement.enabled = true;
    }

    public IEnumerator ChangeRemainingTime(float time)
    {
        if (time < 0f)
        {
            countdownTimerChangesText.color = Color.red;
            countdownTimerChangesText.text = time.ToString() + "s";
        }
        else
        {
            countdownTimerChangesText.color = Color.green;
            countdownTimerChangesText.text = "+" + time.ToString() + "s";
        }
        countdownTimerChangesText.gameObject.SetActive(true);

        for (int i = 0; i < Mathf.Abs(time); i++)
        {
            if (time < 0f)
            {
                countdownTimerText.color = Color.red;
                remainingTime -= 1f;
            }
            else
            {
                countdownTimerText.color = Color.green;
                remainingTime += 1f;
            }
            yield return new WaitForSeconds(1 / (Mathf.Abs(time) * 1.5f));
        }
        countdownTimerText.color = Color.black;
        yield return new WaitForSeconds(0.5f);
        countdownTimerChangesText.gameObject.SetActive(false);
    }

    public void CloseInstructionsPanel()
    {
        //play the button sound when button pressed
        PlayButtonSound();

        // Locks cursor to center of game window and also hides cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Starts the timer automatically
        isTimerRunning = true;

        thirdPersonCamera.enabled = true;
        playerMovement.enabled = true;
        instructionsPanel.SetActive(false);
    }

    public void ShowRules()
    {
        //play the button sound when button pressed
        PlayButtonSound();
        staticRulesPanel.SetActive(true);
    }

    public void CloseRules()
    {
        //play the button sound when button pressed
        PlayButtonSound();
        staticRulesPanel.SetActive(false);
    }

    public void SetCameraSensitivity(float camSens)
    {
        //play the button sound when button pressed
        PlayButtonSound();
        thirdPersonCamera.m_XAxis.m_MaxSpeed = camSens;
        camSensText.text = camSens.ToString();
        PlayerPrefs.SetFloat("CameraSensitivity", camSens);
    }

    public void SetPlayerMoveSpeed(float moveSpeed)
    {
        //play the button sound when button pressed
        PlayButtonSound();
        playerMovement.speed = moveSpeed;
        playerMoveSpeedText.text = moveSpeed.ToString();
        PlayerPrefs.SetFloat("PlayerMoveSpeed", moveSpeed);
    }

    public void SetQuality(int qualityIndex)
    {
        //play the button sound when button pressed
        PlayButtonSound();

        QualitySettings.SetQualityLevel(qualityIndex);
        //Debug.Log(QualitySettings.GetQualityLevel());
    }
}