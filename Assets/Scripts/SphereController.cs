using UnityEngine;
using UnityEngine.InputSystem;

public class SphereController : MonoBehaviour
{
    private MeshRenderer sphereRenderer;
    private bool playerInRange = false;

    void Start()
    {
        sphereRenderer = GetComponent<MeshRenderer>();
        sphereRenderer.enabled = false;
    }

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            sphereRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player is in range");
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player is out of range");
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}