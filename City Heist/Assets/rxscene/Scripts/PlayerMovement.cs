using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    Animator animator;

    public float speed;

    float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;

    public GameObject moneyBag;

    bool firstFall = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Respawn Player If Fall Though Floor
        if (transform.position.y < -10f && !firstFall)
        {
            // Try to respawn at fall point
            velocity.y = -2f;
            controller.enabled = false;
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            controller.enabled = true;
            firstFall = true;
        }
        else if (transform.position.y < -10f && firstFall)
        {
            // Respawn at starting point if failed to respawn at fall point
            velocity.y = -2f;
            controller.enabled = false;
            transform.position = new Vector3(15f, 1f, -15.9f);
            controller.enabled = true;
            firstFall = false;
        }

        // Move Player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        animator.SetFloat("moveSpeed", direction.magnitude);
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            // The player rotates clockwise when shooting while walking for some reason
            //animator.enabled = false;
            //animator.transform.localRotation = Quaternion.identity;
            //animator.enabled = true;
        }

        // Add Gravity to Player
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            if (firstFall)
            {
                firstFall = false;
            }
        }

        velocity.y += (gravity * 2) * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // Rotate Player Towards Mouse Cursor
        /*        Vector3 mousePos = Input.mousePosition;
                mousePos.z = 5.23f;

                Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
                mousePos.x = mousePos.x - objectPos.x;
                mousePos.y = mousePos.y - objectPos.y;

                float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));*/

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
