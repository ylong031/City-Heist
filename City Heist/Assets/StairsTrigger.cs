using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    [SerializeField] int triggerNum;
    [SerializeField] MinimapCamera minimapcamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            minimapcamera.level = triggerNum;
            GameManager.instance.currentLevel = triggerNum;
            if (triggerNum == 1)
            {
                minimapcamera.transform.position = new Vector3(-3.7f, minimapcamera.transform.position.y, -22.22f);
            }
            else if (triggerNum == 2)
            {
                minimapcamera.transform.position = new Vector3(-45.3f, minimapcamera.transform.position.y, -16.22f);
            }
        }
    }
}
