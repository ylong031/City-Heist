using UnityEngine;

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
}
