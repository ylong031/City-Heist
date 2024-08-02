﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColourSquare : MonoBehaviour
{
    public int index;

    private void Update()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Input.mousePosition.x, Input.mousePosition.y)) > 90f)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (index == GameManager.instance.currentIndex)
            {
                GameManager.instance.currentIndex++;
                GetComponent<Image>().color = Color.green;
                if(GameManager.instance.currentIndex == GameManager.instance.finalIndex)
                {
                    GameManager.instance.vaultDoor.OpenVaultDoor();
                }
            }
            else
            {
                GameManager.instance.currentIndex = 1;
                GameManager.instance.remainingTime -= GameManager.instance.colourSquareTaskPenalty;
                StartCoroutine(IncorrectPath());
            }
        }
    }

    IEnumerator IncorrectPath()
    {
        foreach (Image colourSquare in GameManager.instance.colourSquares)
        {
            colourSquare.GetComponent<Image>().color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (Image colourSquare in GameManager.instance.colourSquares)
        {
            colourSquare.GetComponent<Image>().color = Color.white;
        }
    }
}