using UnityEngine;
using System.Collections;

public class RandomizeColourSquares : MonoBehaviour
{
    int horizontalMove = 0;
    int verticalMove = 0;
    [HideInInspector]
    public int moveCount = 0;

    void Start()
    {
        StartCoroutine(Randomize());
    }

    IEnumerator Randomize()
    {
        var rand = Random.Range(0, 2);
        if(rand == 0)
        {
            horizontalMove++;
            if (horizontalMove >= 8)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + (GameManager.instance.colourSquares[9].transform.position.y - GameManager.instance.colourSquares[0].transform.position.y), transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + (GameManager.instance.colourSquares[1].transform.position.x - GameManager.instance.colourSquares[0].transform.position.x), transform.position.y, transform.position.z);
            }
        }
        else if (rand == 1)
        {
            verticalMove++;
            if (verticalMove >= 3)
            {
                transform.position = new Vector3(transform.position.x + (GameManager.instance.colourSquares[1].transform.position.x - GameManager.instance.colourSquares[0].transform.position.x), transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + (GameManager.instance.colourSquares[9].transform.position.y - GameManager.instance.colourSquares[0].transform.position.y), transform.position.z);
            }
        }

        moveCount++;
        if (moveCount >= 10)
        {
            //Debug.Log("Randomization Complete!");
            yield return new WaitForSeconds(0.05f);
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(Randomize());
    }
}
