using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform gunShootPoint;

    public int maxBulletCount;
    int currentBulletCount;
    public float reloadTime;
    bool isReloading = false;

    public GameObject emptyGunMagazine;
    public Transform reloadMagazineDropPoint;

    void Start()
    {
        currentBulletCount = maxBulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        // If you are reloading
        if (isReloading)
        {
            // You can't shoot
            return;
        }

        // If there are bullets in your gun
        if (currentBulletCount > 0)
        {
            // If Left Mouse Button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // Shoot a bullet
                Shoot();
            }
        }
        else
        {
            // Reload gun
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        currentBulletCount--;
        GameObject bulletObj = Instantiate(bullet, gunShootPoint.position, Quaternion.identity);
        bulletObj.GetComponent<Rigidbody>().velocity = transform.forward * bullet.GetComponent<Bullet>().speed;
    }

    IEnumerator Reload()
    {
        GameObject gunMagObj = Instantiate(emptyGunMagazine, reloadMagazineDropPoint.position, reloadMagazineDropPoint.rotation);
        gunMagObj.GetComponent<Rigidbody>().velocity = -transform.up - transform.forward;
        yield return new WaitForSeconds(reloadTime);
        currentBulletCount = maxBulletCount;
        isReloading = false;
    }
}
