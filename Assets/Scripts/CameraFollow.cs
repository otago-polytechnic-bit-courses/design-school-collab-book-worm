using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 8f;
    public float smoothSpeed = 8f;
    public float mouseSensitivity = 3f;

    [Header("Vertical Limits")]
    public float minPitch = 10f;
    public float maxPitch = 70f;

    private float yaw = 0f;
    private float pitch = 20f;

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * mouseSensitivity * 0.1f;
        pitch -= mouseDelta.y * mouseSensitivity * 0.1f;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Camera yaw follows worm's facing direction + mouse offset
        float targetYaw = target.eulerAngles.y + yaw;
        Quaternion rotation = Quaternion.Euler(pitch, targetYaw, 0f);
        Vector3 desiredPosition = target.position + rotation * new Vector3(0f, 0f, -distance);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 0.5f);
    }
}