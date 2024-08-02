using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDRay : MonoBehaviour
{

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray myRay = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            if(Physics.Raycast(myRay, out RaycastHit hit))
            {
                print("This is " + hit.transform.name + " object.");
            }
        }
    }
}
