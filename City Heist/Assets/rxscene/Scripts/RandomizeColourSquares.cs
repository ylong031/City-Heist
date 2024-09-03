using UnityEngine;
using System.Collections;

public class RandomizeColourSquares : MonoBehaviour
{
    int horizontalMove = 0;
    int verticalMove = 0;
    [HideInInspector]
    public int moveCount = 0;

	int continuousHorizontalMove = 0;
	int continuousVerticalMove = 0;

    void Start()
    {
        StartCoroutine(Randomize());
    }

    IEnumerator Randomize()
    {
		if(continuousHorizontalMove == 2 && verticalMove < 3)
		{
            verticalMove++;
			++continuousVerticalMove;
			continuousHorizontalMove = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y + (GameManager.instance.colourSquares[9].transform.position.y - GameManager.instance.colourSquares[0].transform.position.y), transform.position.z);
		}
		else if(continuousVerticalMove == 1 && horizontalMove < 8)
		{
			horizontalMove++;
			++continuousHorizontalMove;
			continuousVerticalMove = 0;
			transform.position = new Vector3(transform.position.x + (GameManager.instance.colourSquares[1].transform.position.x - GameManager.instance.colourSquares[0].transform.position.x), transform.position.y, transform.position.z);
		}
		else
		{
			var rand = Random.Range(0, 2);
			if(rand == 0)
			{
				if (horizontalMove >= 8)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y + (GameManager.instance.colourSquares[9].transform.position.y - GameManager.instance.colourSquares[0].transform.position.y), transform.position.z);
				}
				else
				{
					horizontalMove++;
					++continuousHorizontalMove;
					continuousVerticalMove = 0;
					transform.position = new Vector3(transform.position.x + (GameManager.instance.colourSquares[1].transform.position.x - GameManager.instance.colourSquares[0].transform.position.x), transform.position.y, transform.position.z);
				}
			}
			else if (rand == 1)
			{
				if (verticalMove >= 3)
				{
					transform.position = new Vector3(transform.position.x + (GameManager.instance.colourSquares[1].transform.position.x - GameManager.instance.colourSquares[0].transform.position.x), transform.position.y, transform.position.z);
				}
				else
				{
					verticalMove++;
					++continuousVerticalMove;
					continuousHorizontalMove = 0;
					transform.position = new Vector3(transform.position.x, transform.position.y + (GameManager.instance.colourSquares[9].transform.position.y - GameManager.instance.colourSquares[0].transform.position.y), transform.position.z);
				}
			}
		}
		//Debug.Log("Vert: " + continuousVerticalMove + " | Hori: " + continuousHorizontalMove);

        moveCount++;
        if (moveCount >= 10)
        {
            //Debug.Log("Randomization Complete!");
            yield return new WaitForSeconds(0.05f);
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Randomize());
    }
}
