using UnityEngine;

public class BossDoorController : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CharacterController player = other.GetComponent<CharacterController>();

        if (player != null)
        {
            player.transform.position = new Vector3(
                player.transform.position.x,
                -50f,
                player.transform.position.z
            );
        }
    }
}