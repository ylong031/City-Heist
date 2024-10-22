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

    private AudioSource smokeAudioSource;

    [SerializeField] GameObject thirdpersoncamera;

    [SerializeField] GameObject firstpersoncamera;


    private bool stop;



    void Start()
    {
        //get audio from the smoke object
        smokeAudioSource = smoke.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        speedtext.text = "Speed : " + speed;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (thirdpersoncamera.activeSelf)
            {
                thirdpersoncamera.SetActive(false);
                firstpersoncamera.SetActive(true);

            }
            else
            {
                thirdpersoncamera.SetActive(true);
                firstpersoncamera.SetActive(false);


            }





        }


        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (speed < 0)
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0f);
            }
            else
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0f);
            }



        }
        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            if (speed < 0)
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0f);
            }
            else
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0f);
            }



        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))    
        {
            if (speed >= 25) return;
            speed += 0.5f;
            
            

        }
        if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.S))
        {
            if (speed <= -25) return;
            speed -= 0.5f;

        }

        if (Input.GetKey(KeyCode.E))
        {
            
            stop = true;

        }

        if (stop) 
        {
            if (speed > 0) 
            {
                speed = speed - 0.5f;


            }
            if (speed < 0)
            {
                speed = speed + 0.5f;


            }


        }
        if (speed == 0) 
        {
            stop = false;

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
            stop = true;

            smoke.GetComponent<ParticleSystem>().Play();

            //play the audio for car crash
            if (smokeAudioSource != null)
            {
                smokeAudioSource.Play();
            }

            misc.GetComponent<Misc>().Deduct();



        }
    }

}
