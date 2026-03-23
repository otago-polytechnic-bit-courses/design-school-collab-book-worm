using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetTrigger("AttackingX");
        }
    }
    public void OnAltAttack(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetTrigger("AttackingY");
        }
    }
}
