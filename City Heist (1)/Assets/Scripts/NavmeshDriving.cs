using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshDriving : MonoBehaviour
{
    [SerializeField] GameObject waypoints;
    NavMeshAgent agent;
    Transform[] alltransforms;
   
    int i = 0;
    void Start()
    {
        alltransforms = waypoints.GetComponent<Waypoints>().alltransforms;
        agent=GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
     
        agent.SetDestination(alltransforms[i].position);
        if (Vector3.Distance(transform.position, alltransforms[i].position)<0.5f)
        {
           
            i++;
            if (i == alltransforms.Length)
            {
                i = 0;

            }

        }




    }
}
