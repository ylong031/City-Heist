using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;

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

    // Countdown Timer
    public float remainingTime = 180f;
    public bool isTimerRunning = false;
    public TMP_Text countdownTimerText;

    public GameObject youLosePanel;

    public PlayerMovement playerMovement;
    public CinemachineFreeLook thirdPersonCamera;

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

    // Rewards (Time)
    public float jamCCTVReward;
    public float takeHostageReward;

    // Penalties (Time)
    public float killHostagePenalty;
    public float vaultDoorPenalty;
    public float colourSquareTaskPenalty;
    public float cctvConsolePenalty;

    void Awake()
    {
        // Game Manager Singleton
        instance = this;
    }

    private void Start()
    {
        // Carry over money from city scene
        moneyText.text = "$" + PlayerPrefs.GetFloat("Money").ToString();
        
        // Locks cursor to center of game window and also hides cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Starts the timer automatically
        isTimerRunning = true;

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

        if (!isColourSquareTask)
        {
            // Generate 4-digit bank vault code
            GenerateVaultCode();
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
                youLosePanel.SetActive(true);
                playerMovement.enabled = false;
            }
        }

        // CCTV Panel
        if (cctvPanel.activeSelf && !cctvConsole.isBeingJammed)
        {
            foreach(var wire in wiresToRotate)
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
            yield return new WaitForSeconds(1 / (Mathf.Abs(time) * 2));
        }
        countdownTimerText.color = Color.black;
    }
}
