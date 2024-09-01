using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCamera : MonoBehaviour
{
    public int level;
  
    private int currentlevel=1;

    Camera minicamera;
   
   

    [SerializeField] LayerMask mask1;
    [SerializeField] LayerMask mask2;


    [SerializeField] GameObject lvl1text;
    [SerializeField] GameObject lvl2text;
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
                lvl1text.SetActive(true);
                lvl2text.SetActive(false);

            }
            else 
            {
                minicamera.cullingMask = mask2;
                lvl2text.SetActive(true);
                lvl1text.SetActive(false);
            }
            currentlevel = level;
        }
                
    }
   
}
