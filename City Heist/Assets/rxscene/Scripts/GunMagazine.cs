using UnityEngine;

public class GunMagazine : MonoBehaviour
{
    private void Start()
    {
        // Destroy empty gun magazine after 5s to avoid clutter
        Destroy(gameObject, 5f);
    }
}