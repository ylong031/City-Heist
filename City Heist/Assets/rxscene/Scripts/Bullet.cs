using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        // Destroy bullet after 5s to avoid clutter
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            other.GetComponent<NPC>().TakeDamage(1);
        }
        if (other.tag == "Glass")
        {
            Destroy(other.gameObject);
            Instantiate(GameManager.instance.fracturedGlass, other.transform.position, other.transform.rotation);
        }
        Destroy(gameObject);
    }
}