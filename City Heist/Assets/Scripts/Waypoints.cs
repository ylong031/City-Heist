using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] alltransforms = new Transform[4];

    void Awake()
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            alltransforms[i] = transform.GetChild(i);

        }
    }
}
