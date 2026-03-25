using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 5f;
    public float height = 2f;
    public float mouseSensitivity = 200f;

    private float currentYaw = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        currentYaw += mouseX;

        Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);

        Vector3 offset = rotation * new Vector3(0, height, -distance);

        transform.position = target.position + offset;

        transform.LookAt(target);
    }
}