using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform[] gunShootPoint;

    public int maxBulletCount;
    int[] currentBulletCount;
    public float reloadTime;
    bool isReloading = false;

    public GameObject emptyGunMagazine;
    public Transform[] reloadMagazineDropPoint;

    public TMP_Text bulletCountText;

    public bool isAuto = false;
    float shootTime;
    public float bulletsPerSecond;

    public GameObject[] guns;
    int currentGunIndex = 0;

    void Start()
    {
        //currentBulletCount = maxBulletCount;
        currentBulletCount = new int[3];
        // Pistol
        currentBulletCount[0] = 12;
        // AK
        currentBulletCount[1] = 40;
        // M4
        currentBulletCount[2] = 30;
        bulletCountText.text = currentBulletCount[currentGunIndex] + "/∞";
    }

    // Update is called once per frame
    void Update()
    {
        shootTime -= Time.deltaTime;

        // If you are reloading / doing puzzle task / game is paused
        if (isReloading || GameManager.instance.cctvPanel.activeSelf || GameManager.instance.vaultKeypad.activeSelf || GameManager.instance.colourSquarePanel.activeSelf || GameManager.instance.instructionsPanel.activeSelf || PauseGame.instance.isPaused)
        {
            // You can't shoot / cycle guns
            return;
        }

        // Cycle guns with mouse scroll wheel
        /*        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // up
                {
                    if (currentGunIndex == 0)
                    {
                        currentGunIndex = 2;
                    }
                    else
                    {
                        currentGunIndex--;
                    }

                    // Pistol
                    if (currentGunIndex == 0)
                    {
                        guns[1].SetActive(false);
                        guns[2].SetActive(false);
                        guns[0].SetActive(true);
                        isAuto = false;
                        maxBulletCount = 12;
                        reloadTime = 2f;
                        bulletsPerSecond = 9f;
                    }
                    // AK
                    else if (currentGunIndex == 1)
                    {
                        guns[2].SetActive(false);
                        guns[0].SetActive(false);
                        guns[1].SetActive(true);
                        isAuto = true;
                        maxBulletCount = 40;
                        reloadTime = 2.9f;
                        bulletsPerSecond = 10f;
                    }
                    // M4
                    else if (currentGunIndex == 2)
                    {
                        guns[0].SetActive(false);
                        guns[1].SetActive(false);
                        guns[2].SetActive(true);
                        isAuto = true;
                        maxBulletCount = 30;
                        reloadTime = 3.1f;
                        bulletsPerSecond = 11.63f;
                    }
                    bulletCountText.text = currentBulletCount[currentGunIndex] + "/∞";
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // down
                {
                    if (currentGunIndex == 2)
                    {
                        currentGunIndex = 0;
                    }
                    else
                    {
                        currentGunIndex++;
                    }

                    // Pistol
                    if (currentGunIndex == 0)
                    {
                        guns[1].SetActive(false);
                        guns[2].SetActive(false);
                        guns[0].SetActive(true);
                        isAuto = false;
                        maxBulletCount = 12;
                        reloadTime = 2f;
                        bulletsPerSecond = 9f;
                    }
                    // AK
                    else if (currentGunIndex == 1)
                    {
                        guns[2].SetActive(false);
                        guns[0].SetActive(false);
                        guns[1].SetActive(true);
                        isAuto = true;
                        maxBulletCount = 40;
                        reloadTime = 2.9f;
                        bulletsPerSecond = 10f;
                    }
                    // M4
                    else if (currentGunIndex == 2)
                    {
                        guns[0].SetActive(false);
                        guns[1].SetActive(false);
                        guns[2].SetActive(true);
                        isAuto = true;
                        maxBulletCount = 30;
                        reloadTime = 3.1f;
                        bulletsPerSecond = 11.63f;
                    }
                    bulletCountText.text = currentBulletCount[currentGunIndex] + "/∞";
                }*/


        // Cycle guns with C key
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentGunIndex == 2)
            {
                currentGunIndex = 0;
            }
            else
            {
                currentGunIndex++;
            }

            // Pistol
            if (currentGunIndex == 0)
            {
                guns[1].SetActive(false);
                guns[2].SetActive(false);
                guns[0].SetActive(true);
                isAuto = false;
                maxBulletCount = 12;
                reloadTime = 2f;
                bulletsPerSecond = 9f;
            }
            // AK
            else if (currentGunIndex == 1)
            {
                guns[2].SetActive(false);
                guns[0].SetActive(false);
                guns[1].SetActive(true);
                isAuto = true;
                maxBulletCount = 40;
                reloadTime = 2.9f;
                bulletsPerSecond = 10f;
            }
            // M4
            else if (currentGunIndex == 2)
            {
                guns[0].SetActive(false);
                guns[1].SetActive(false);
                guns[2].SetActive(true);
                isAuto = true;
                maxBulletCount = 30;
                reloadTime = 3.1f;
                bulletsPerSecond = 11.63f;
            }
            bulletCountText.text = currentBulletCount[currentGunIndex] + "/∞";
        }

        // If there are bullets in your gun
        if (currentBulletCount[currentGunIndex] > 0)
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
        currentBulletCount[currentGunIndex]--;
        GameObject bulletObj = Instantiate(bullet, gunShootPoint[currentGunIndex].position, Quaternion.identity);
        bulletObj.GetComponent<Rigidbody>().velocity = transform.forward * bullet.GetComponent<Bullet>().speed;

        bulletCountText.text = currentBulletCount[currentGunIndex] + "/∞";
    }

    IEnumerator Reload()
    {
        GameObject gunMagObj = Instantiate(emptyGunMagazine, reloadMagazineDropPoint[currentGunIndex].position, reloadMagazineDropPoint[currentGunIndex].rotation);
        gunMagObj.GetComponent<Rigidbody>().velocity = -transform.up - transform.forward;

        //yield return new WaitForSeconds(reloadTime);
        bulletCountText.text = "Reloading";
        yield return new WaitForSeconds(reloadTime / 4);
        bulletCountText.text = "Reloading.";
        yield return new WaitForSeconds(reloadTime / 4);
        bulletCountText.text = "Reloading..";
        yield return new WaitForSeconds(reloadTime / 4);
        bulletCountText.text = "Reloading...";
        yield return new WaitForSeconds(reloadTime / 4);

        currentBulletCount[currentGunIndex] = maxBulletCount;
        isReloading = false;

        bulletCountText.text = currentBulletCount[currentGunIndex] + "/∞";
    }
}
