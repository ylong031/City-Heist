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

    public bool isAuto = false;
    float shootTime;
    public float bulletsPerSecond;

    void Start()
    {
        currentBulletCount = maxBulletCount;
        bulletCountText.text = currentBulletCount + "/∞";
    }

    // Update is called once per frame
    void Update()
    {
        shootTime -= Time.deltaTime;

        // If you are reloading / doing puzzle task
        if (isReloading || GameManager.instance.cctvPanel.activeSelf || GameManager.instance.vaultKeypad.activeSelf || GameManager.instance.colourSquarePanel.activeSelf || GameManager.instance.instructionsPanel.activeSelf)
        {
            // You can't shoot
            return;
        }

        // If there are bullets in your gun
        if (currentBulletCount > 0)
        {
            if(shootTime <= 0f)
            {
                if (isAuto)
                {
                    // If Left Mouse Button is held
                    if (Input.GetMouseButton(0))
                    {
                        // Shoot a bullet
                        Shoot();
                        shootTime = 1 / bulletsPerSecond;
                    }
                }
                else
                {
                    // If Left Mouse Button is pressed
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Shoot a bullet
                        Shoot();
                        shootTime = 1 / bulletsPerSecond;
                    }
                }
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
