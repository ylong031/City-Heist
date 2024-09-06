using UnityEngine;
using UnityEngine.UI;

public class WiresToRotate : MonoBehaviour
{
    public float correctRot;
    public bool isInCorrectRot = false;
    bool firstTimeCorrect = false;
    public Outline[] wireOutlines;

    public void ChangeWireRotation()
    {
        if (transform.eulerAngles.z == 0f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
        }
        else if (transform.eulerAngles.z == 90f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        }
        else if (transform.eulerAngles.z == 180f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
        }
        else if (transform.eulerAngles.z == 270f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }

        if (transform.eulerAngles.z == correctRot)
        {
            GameManager.instance.taskCorrectAudioSource.Play();

            isInCorrectRot = true;
            firstTimeCorrect = true;

            foreach (var wireOutline in wireOutlines)
            {
                wireOutline.enabled = true;
            }
        }
        else
        {
            foreach (var wireOutline in wireOutlines)
            {
                wireOutline.enabled = false;
            }

            // If wire rotates from correct rotation to incorrect rotation
            if (isInCorrectRot && firstTimeCorrect)
            {
                GameManager.instance.taskIncorrectAudioSource.Play();
                // Incur time penalty
                StartCoroutine(GameManager.instance.ChangeRemainingTime(-GameManager.instance.cctvConsolePenalty));
            }
            isInCorrectRot = false;
        }
    }
}
