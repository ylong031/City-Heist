using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject bulletHole;

    private void Start()
    {
        // Destroy bullet after 5s to avoid clutter
        Destroy(gameObject, 5f);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "NPC")
    //    {
    //        other.GetComponent<NPC>().TakeDamage(1);
    //    }
    //    else if (other.tag == "Glass")
    //    {
    //        Destroy(other.gameObject);
    //        Instantiate(GameManager.instance.fracturedGlass, other.transform.position, other.transform.rotation);
    //    }
    //    else if (other.tag == "WideGlass")
    //    {
    //        Destroy(other.gameObject);
    //        Instantiate(GameManager.instance.wideFracturedGlass, other.transform.position, other.transform.rotation);
    //    }
    //    Destroy(gameObject);
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "NPC")
        {
            collision.collider.GetComponent<NPC>().TakeDamage(1);
        }
        else if (collision.collider.tag == "Glass")
        {
            Destroy(collision.collider.gameObject);
            Instantiate(GameManager.instance.fracturedGlass, collision.collider.transform.position, collision.collider.transform.rotation);
        }
        else if (collision.collider.tag == "WideGlass")
        {
            Destroy(collision.collider.gameObject);
            Instantiate(GameManager.instance.wideFracturedGlass, collision.collider.transform.position, collision.collider.transform.rotation);
        }
        else
        {
            if (collision.collider.tag == "Walls&Doors")
            {
                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
                Vector3 pos = contact.point;
                GameObject bulletHoleObj = Instantiate(bulletHole, pos, rot);
                bulletHoleObj.transform.position = Vector3.MoveTowards(bulletHoleObj.transform.position, GameManager.instance.playerMovement.transform.position, 0.01f);
            }
        }
        Destroy(gameObject);
    }
}