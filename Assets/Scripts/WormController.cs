using UnityEngine;
using UnityEngine.InputSystem;

public class WormController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 8f;

    [Header("Jump")]
    public float minJumpForce = 3f;
    public float maxJumpForce = 10f;
    public float maxCoilTime = 1f;
    public float gravityScale = 2.5f;

    [Header("Ground Check")]
    public float groundCheckDistance = 0.3f;
    public LayerMask groundLayer;

    [Header("Abilities")]
    public bool hasDoubleJump = false;
    private bool usedDoubleJump = false;

    [Header("Dash")]
    public bool hasDash = false;
    public float dashForce = 15f;
    public float dashDuration = 0.15f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private bool usedDash = false;

    private Rigidbody rb;
    private float coilTime = 0f;
    private bool isGrounded = false;
    private Vector3 currentVelocity;

    // Input System
    private InputSystem_Actions inputActions;
    private Vector2 moveInput;
    private bool jumpHeld = false;
    private bool jumpReleased = false;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Sprint.performed += ctx =>
        {
            Debug.Log("Sprint pressed, hasDash: " + hasDash + " isGrounded: " + isGrounded + " usedDash: " + usedDash);
            HandleDash();
        };

        inputActions.Player.Jump.performed += ctx => jumpHeld = true;
        inputActions.Player.Jump.canceled += ctx =>
        {
            jumpReleased = true;
            jumpHeld = false;
        };
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CheckGround();
        HandleCoilJump();
        HandleDashTimer();
    }

    void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
    }
void HandleMovement()
{
    // Don't override velocity during dash
    if (isDashing) return;

    transform.Rotate(0f, moveInput.x * 150f * Time.fixedDeltaTime, 0f);

    Vector3 moveDirection = transform.forward * moveInput.y;
    Vector3 targetVelocity = moveDirection * moveSpeed;

    float rate = moveInput.magnitude > 0 ? acceleration : deceleration;
    currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, rate * Time.fixedDeltaTime);

    rb.linearVelocity = new Vector3(currentVelocity.x, rb.linearVelocity.y, currentVelocity.z);
}

    void HandleCoilJump()
    {
        // Reset double jump when grounded
        if (isGrounded) usedDoubleJump = false;

        // Wind up while held and grounded
        if (jumpHeld && isGrounded)
        {
            coilTime += Time.deltaTime;
            coilTime = Mathf.Clamp(coilTime, 0, maxCoilTime);
        }

        if (jumpReleased)
        {
            if (isGrounded)
            {
                // Normal coil jump
                float t = coilTime / maxCoilTime;
                float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, t);
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                coilTime = 0f;
            }
            else if (hasDoubleJump && !usedDoubleJump)
            {
                // Double jump - mid air smaller jump
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, minJumpForce * 1.5f, rb.linearVelocity.z);
                usedDoubleJump = true;
            }
        }

        jumpReleased = false;
    }

    void HandleDash()
    {
        if (hasDash && !isGrounded && !usedDash)
        {
            isDashing = true;
            dashTimer = dashDuration;
            usedDash = true;

            Vector3 dashDirection = transform.forward;
            rb.linearVelocity = new Vector3(dashDirection.x * dashForce, 0f, dashDirection.z * dashForce);
            Debug.Log("Dashing with force: " + dashForce);
        }
    }

    void HandleDashTimer()
    {
        if (isGrounded) usedDash = false;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }
    }

    void ApplyGravity()
    {
        // Suppress gravity completely during dash
        if (isDashing) return;
        rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }

    public void UnlockAbility(AbilityType ability)
    {
        if (ability == AbilityType.DoubleJump)
        {
            hasDoubleJump = true;
            Debug.Log("Double jump unlocked!");
        }
        else if (ability == AbilityType.Dash)
        {
            hasDash = true;
            Debug.Log("Dash unlocked!");
        }
    }
}