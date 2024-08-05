
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Camera cam;
    Rigidbody rb;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         //Vector2 mousePos = Input.mousePosition;
         //Vector3 worldPoint = new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y);
         //Vector3 target = cam.ScreenToWorldPoint(worldPoint);
         //transform.LookAt(target);
    }

    private void FixedUpdate()//50 times per second
    {
        float forward = Input.GetAxis("Vertical");
        float side = Input.GetAxis("Horizontal");

        Vector3 newPos = new Vector3(side, 0, forward) * movementSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + newPos);

        if (forward == 0 && side == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}

