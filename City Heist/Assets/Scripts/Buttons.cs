using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play() 
    {
        SceneManager.LoadScene(1);
    
    
    }

    public void Return()
    {
        SceneManager.LoadScene(0);


    }

    public void Exit() 
    {
    
    
    }

    public void Rules() 
    {
    
        Debug.Log("Rules!");
    }
}
