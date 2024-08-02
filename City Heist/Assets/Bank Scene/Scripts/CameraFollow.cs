
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector
    public Vector3 offset;   // Offset from the player's position

    void Start()
    {
        // Calculate initial offset
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Ensure camera follows player position with the initial offset
        Vector3 newPosition = player.position + offset;
        transform.position = new Vector3(newPosition.x, offset.y, newPosition.z);
    }
}
