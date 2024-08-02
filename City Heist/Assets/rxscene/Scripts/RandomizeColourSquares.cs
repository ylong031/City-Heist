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
                transform.position = new Vector3(transform.position.x, transform.position.y + 219, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 219, transform.position.y, transform.position.z);
            }
        }
        else if (rand == 1)
        {
            verticalMove++;
            if (verticalMove >= 3)
            {
                transform.position = new Vector3(transform.position.x + 219, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 219, transform.position.z);
            }
        }

        moveCount++;
        if (moveCount >= 10)
        {
            //Debug.Log("End Reached!");
            yield return new WaitForSeconds(0.05f);
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(Randomize());
    }
}
