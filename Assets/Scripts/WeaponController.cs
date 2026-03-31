using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WeaponController : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy;
    private InputAction m_attackAction;
    public InputActionAsset inputActions;
    public int damage = 1;
    private bool attackPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        if (m_attackAction.IsPressed())
        {
            attackPressed = true;
        } else
        {
                       attackPressed = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (attackPressed == true)
            {
                enemy.health -= damage;
                Debug.Log("Enemy hit! Remaining Enemy health: " + enemy.health);
            }
           
           

        }

    }

}
