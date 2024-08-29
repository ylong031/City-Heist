using System.Collections;
using System.Collections.Generic;
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

        }
    }
}
