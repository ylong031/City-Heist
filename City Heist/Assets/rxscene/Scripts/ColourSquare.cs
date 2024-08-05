using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColourSquare : MonoBehaviour
{
    public int index;

    private void Update()
    {
        if (GameManager.instance.randomizer != null && Vector2.Distance(transform.position, GameManager.instance.randomizer.transform.position) <= 5f)
        {
            index = GameManager.instance.randomizer.moveCount;
        }
    }

    public void ChangeSquareColour()
    {
        if (index == GameManager.instance.currentIndex)
        {
            GameManager.instance.currentIndex++;
            GetComponent<Image>().color = Color.green;
            if (GameManager.instance.currentIndex == GameManager.instance.finalIndex)
            {
                StartCoroutine(GameManager.instance.vaultDoor.OpenVaultDoor());
            }
        }
        else
        {
            GameManager.instance.currentIndex = 1;
            //GameManager.instance.remainingTime -= GameManager.instance.colourSquareTaskPenalty;
            StartCoroutine(GameManager.instance.ChangeRemainingTime(-GameManager.instance.colourSquareTaskPenalty));
            StartCoroutine(IncorrectPath());
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