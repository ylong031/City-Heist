using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Driving : MonoBehaviour
{
    public float speed = 0f;

    [SerializeField] private float turnSpeed;


    [SerializeField] TMP_Text speedtext;

    [SerializeField] GameObject smoke;

    [SerializeField] GameObject misc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        speedtext.text = "Speed : " + speed;
     
      
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0f);


        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0 , turnSpeed * Time.deltaTime, 0f);


        }
        if (Input.GetKey(KeyCode.UpArrow))    
        {
            if (speed >= 20) return;
            speed += 2;
            
            

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (speed <= -20) return;
            speed -= 2;

        }

        if (Input.GetKey(KeyCode.S))
        {
            speed = 0;

        }



    }

    private void FixedUpdate()
    {
         GetComponent<Rigidbody>().velocity = transform.forward * speed;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Vehicle"))
        {
            speed = 0;

            smoke.GetComponent<ParticleSystem>().Play();

            misc.GetComponent<Misc>().Deduct();



        }
    }

}
