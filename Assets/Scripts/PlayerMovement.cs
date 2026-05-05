using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 720f;
    public Transform cameraTransform;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Vector3 currentPostion;
    bool isDashing = false;

    private Vector3 velocity;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Read input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(x, 0f, z).normalized;

        // Handle gravity and jumping
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // small negative to keep grounded
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {

            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            StartCoroutine(Dash(15f, 0.2f));
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        //  Camera-relative movement
        Vector3 moveDirection = Vector3.zero;

        if (inputDirection.magnitude >= 0.1f)
        {
            // Get camera directions
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            // Convert input into camera-relative direction
            moveDirection = camForward * z + camRight * x;

            // Rotate player toward movement direction
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Combine horizontal and vertical movement
        Vector3 finalMove = moveDirection * speed + new Vector3(0f, velocity.y, 0f);

        // Move the character
        controller.Move(finalMove * Time.deltaTime);
        currentPostion = transform.position;
    }
    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    IEnumerator Dash(float dashSpeed, float time)
    {
        isDashing = true;

        float orignalSpeed = speed;
        speed = dashSpeed;

        yield return new WaitForSeconds(time);

        // reset speed
        speed = orignalSpeed;
        isDashing = false;

    }

    public Vector3 getPosition()
    {
        return currentPostion;
    }
}
    