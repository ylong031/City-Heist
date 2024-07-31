using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Misc : MonoBehaviour
{
    static float money=1000;
    [SerializeField] TMP_Text moneytext;
    [SerializeField] GameObject taskmenu;

    public void Deduct() 
    {
        money = money - 10;
    
    
    }
    private void Update()
    {
        moneytext.text = "$ " + money;

        if (Input.GetKeyDown(KeyCode.T))
        {
            if(taskmenu.activeSelf) 
            {
                taskmenu.SetActive(false);

            }
            else 
            {
                taskmenu.SetActive(true);
            
            }
           

        }
    }
}
