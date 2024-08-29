using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public int level;
  
    private int currentlevel=1;

    Camera minicamera;
   
   

    [SerializeField] LayerMask mask1;
    [SerializeField] LayerMask mask2;

    private void Awake()
    {
        minicamera = GetComponent<Camera>();
        
    }


    void Update()
    {
        if(level != currentlevel)
        {
            if(level== 1)
            {
                minicamera.cullingMask = mask1;

            }
            else 
            {
                minicamera.cullingMask = mask2;

            }
            currentlevel = level;
        }
                
    }
   
}
