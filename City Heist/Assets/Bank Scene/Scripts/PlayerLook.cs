using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    Camera cam;
    Rigidbody rb;
    Ray mouseRay;
    Vector3 hitPoint;

    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(mouseRay, out hitInfo, 100f))
        {
            hitPoint = hitInfo.point;
        }
        Vector3 looktarget = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
        transform.LookAt(looktarget);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 dropPoint = new Vector3(hitPoint.x, hitPoint.y + 1, hitPoint.z);
            Instantiate(ball, dropPoint, Quaternion.identity);
        }
    }
}