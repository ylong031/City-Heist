using UnityEngine;
using UnityEngine.SceneManagement;

public class Minimaps : MonoBehaviour
{
    public GameObject[] minimaps;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("EnableMinimap", 1) == 0)
        {
            foreach (var minimap in minimaps)
            {
                minimap.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "CityScene")
        {
            return;
        }

        if (SceneManager.GetActiveScene().name == "Bank 1")
        {
            // If Zoomed In Enough, Minimap Will Follow Player
            if (minimaps[0].GetComponent<Camera>().orthographicSize <= 19.45f)
            {
                minimaps[0].transform.position = new Vector3(GameManager.instance.playerMovement.transform.position.x, minimaps[0].transform.position.y, GameManager.instance.playerMovement.transform.position.z);
            }

            if (minimaps[0].GetComponent<Camera>().orthographicSize > 15.46f && Input.GetAxis("Mouse ScrollWheel") > 0f) // up
            {
                // Zoom In
                minimaps[0].GetComponent<Camera>().orthographicSize -= 2f;
                minimaps[0].transform.position = new Vector3(minimaps[0].transform.position.x - (-3.7f - GameManager.instance.playerMovement.transform.position.x) / 5, minimaps[0].transform.position.y, minimaps[0].transform.position.z - (-22.22f - GameManager.instance.playerMovement.transform.position.z) / 5);
            }
            else if (minimaps[0].GetComponent<Camera>().orthographicSize < 25.45f && Input.GetAxis("Mouse ScrollWheel") < 0f) // down
            {
                // Zoom Out
                minimaps[0].GetComponent<Camera>().orthographicSize += 2f;
                minimaps[0].transform.position = new Vector3(minimaps[0].transform.position.x + (-3.7f - GameManager.instance.playerMovement.transform.position.x) / 5, minimaps[0].transform.position.y, minimaps[0].transform.position.z + (-22.22f - GameManager.instance.playerMovement.transform.position.z) / 5);
                if (minimaps[0].GetComponent<Camera>().orthographicSize == 25.45f)
                {
                    minimaps[0].transform.position = new Vector3(-3.7f, minimaps[0].transform.position.y, -22.22f);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Bank 2")
        {
            // If Zoomed In Enough, Minimap Will Follow Player
            if (minimaps[0].GetComponent<Camera>().orthographicSize <= 23.8f)
            {
                minimaps[0].transform.position = new Vector3(GameManager.instance.playerMovement.transform.position.x, minimaps[0].transform.position.y, GameManager.instance.playerMovement.transform.position.z);
            }

            if (minimaps[0].GetComponent<Camera>().orthographicSize > 19.81f && Input.GetAxis("Mouse ScrollWheel") > 0f) // up
            {
                // Zoom In
                minimaps[0].GetComponent<Camera>().orthographicSize -= 2f;
                minimaps[0].transform.position = new Vector3(minimaps[0].transform.position.x - (-9.1f - GameManager.instance.playerMovement.transform.position.x) / 5, minimaps[0].transform.position.y, minimaps[0].transform.position.z - (-19.64f - GameManager.instance.playerMovement.transform.position.z) / 5);
            }
            else if (minimaps[0].GetComponent<Camera>().orthographicSize < 29.8f && Input.GetAxis("Mouse ScrollWheel") < 0f) // down
            {
                // Zoom Out
                minimaps[0].GetComponent<Camera>().orthographicSize += 2f;
                minimaps[0].transform.position = new Vector3(minimaps[0].transform.position.x + (-9.1f - GameManager.instance.playerMovement.transform.position.x) / 5, minimaps[0].transform.position.y, minimaps[0].transform.position.z + (-19.64f - GameManager.instance.playerMovement.transform.position.z) / 5);
                if (minimaps[0].GetComponent<Camera>().orthographicSize == 29.8f)
                {
                    minimaps[0].transform.position = new Vector3(-9.1f, minimaps[0].transform.position.y, -19.64f);
                }
            }
        }
    }
}
