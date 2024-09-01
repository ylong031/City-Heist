using UnityEngine;

public class CCTVIndicator : MonoBehaviour
{
    public GameObject[] cctvIndicators;

    void Start()
    {
        foreach (var cctvIndicator in cctvIndicators)
        {
            cctvIndicator.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.instance.jammedCCTV)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !GameManager.instance.jammedCCTV)
        {
            foreach (var cctvIndicator in cctvIndicators)
            {
                cctvIndicator.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !GameManager.instance.jammedCCTV)
        {
            foreach (var cctvIndicator in cctvIndicators)
            {
                cctvIndicator.SetActive(false);
            }
        }
    }
}
