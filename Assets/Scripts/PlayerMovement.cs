using System.Threading;
using Unity.VectorGraphics;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Camera cam;

    [Header("PlayerInput")]
    //Get Action list
    public InputActionAsset inputActions;
    //Get individual actions and store them
    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;


    //Store the X-Y values of the inputs
    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;
    //private Animator m_animator;
    private Rigidbody m_rigidbody;

    //
    public float walkSpeed = 5;
    public float rotateSpeed = 5;
    public float jumpSpeed = 5;

    /// <summary>
    /// To use an action map, it must be enabled within the OnEnable method.
    /// While enabled, all actions are monitored during the Input System Update Logic. Every frame.
    /// </summary>
    /// 


    //Player health 
    public int health = 30;
    public int damage = 1;
[SerializeField] EnemyMovement enemy;
    public string scene = "SampleScene";
    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    /// <summary>
    /// If object is destroyed, disable the map
    /// </summary>
    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_jumpAction = InputSystem.actions.FindAction("Jump");

        //m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();

        //Mouse related content
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }
    private void Update()
    {

        //Input Action related content
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

        if (m_jumpAction.WasPressedThisFrame())
        {
            Jump();
        }


    }
    
    private void Jump()
    {
        m_rigidbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
        //m_animator.SetTrigger("Jump");
    }
    private void Walking()
    {
        //m_animator.SetFloat("Speed", m_moveAmt.y);
        m_rigidbody.MovePosition(m_rigidbody.position + transform.forward * m_moveAmt.y * walkSpeed * Time.deltaTime);
        m_rigidbody.MovePosition(m_rigidbody.position + transform.right * m_moveAmt.x * walkSpeed * Time.deltaTime);
    }
    private void Rotating()
    {
        if (m_lookAmt.x != 0)
        {
            float rotationAmount = m_lookAmt.x * rotateSpeed * Time.deltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
            m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= enemy.damage;
            Debug.Log("Player Health: " + health);
            if (health <= 0)
            {
                Debug.Log("Player is Dead!");
                Destroy(gameObject);
                Thread.Sleep(2000); // Wait for 2 seconds before implementing death logic
                SceneManager.LoadScene(scene);
                // Implement player death logic here (e.g., respawn, game over screen, etc.)
            }

        }
    }


}
