using System.Collections;
using TMPro;
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

    public TMP_Text bulletCountText;

    void Start()
    {
        currentBulletCount = maxBulletCount;
        bulletCountText.text = currentBulletCount + "/∞";
    }

    // Update is called once per frame
    void Update()
    {
        // If you are reloading / doing puzzle task
        if (isReloading || GameManager.instance.cctvPanel.activeSelf || GameManager.instance.vaultKeypad.activeSelf || GameManager.instance.colourSquarePanel.activeSelf)
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

        bulletCountText.text = currentBulletCount + "/∞";
    }

    IEnumerator Reload()
    {
        GameObject gunMagObj = Instantiate(emptyGunMagazine, reloadMagazineDropPoint.position, reloadMagazineDropPoint.rotation);
        gunMagObj.GetComponent<Rigidbody>().velocity = -transform.up - transform.forward;
        yield return new WaitForSeconds(reloadTime);
        currentBulletCount = maxBulletCount;
        isReloading = false;

        bulletCountText.text = currentBulletCount + "/∞";
    }
}
