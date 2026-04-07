using UnityEngine;

public enum AbilityType
{
    DoubleJump,
    WallJump,
    Dash
}

public class Collectible : MonoBehaviour
{
    public AbilityType abilityType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WormController worm = other.GetComponent<WormController>();
            if (worm != null)
            {
                worm.UnlockAbility(abilityType);
                Destroy(gameObject);
            }
        }
    }
}