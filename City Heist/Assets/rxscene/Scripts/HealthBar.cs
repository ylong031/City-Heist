using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    Image fillImage;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        fillImage = GetComponentInChildren<Image>();
        fillImage.enabled = false;
    }

    void Update()
    {
        // Rotate health bar towards player so the player can see it from any angle
        //transform.LookAt(GameManager.instance.playerMovement.transform, Vector3.up);
        Vector3 targetPos = new Vector3(GameManager.instance.playerMovement.transform.position.x,
                                       transform.position.y,
                                       GameManager.instance.playerMovement.transform.position.z);
        transform.LookAt(targetPos, Vector3.up);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public IEnumerator SetHealth(float dmg)
    {
        fillImage.enabled = true;

        for (int i = 0; i < 10; i++)
        {
            slider.value -= dmg / 10;
            yield return new WaitForSeconds(0.03f);
        }

        if (slider.value < 1.01f)
        {
            fillImage.color = Color.red;
        }
    }

    public void SetHealthToZero()
    {
        slider.value = 0;
        fillImage.enabled = false;
    }
}
