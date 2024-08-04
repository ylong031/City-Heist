using UnityEngine;
using UnityEngine.EventSystems;

public class WiresToRotate : MonoBehaviour
{
    public float correctRot;
    public bool isInCorrectRot = false;

    /*private void Update()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Input.mousePosition.x, Input.mousePosition.y)) > 75f)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ChangeWireRotation();
        }
    }*/

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
            isInCorrectRot = true;
        }
        else
        {
            isInCorrectRot = false;
        }
    }
}
