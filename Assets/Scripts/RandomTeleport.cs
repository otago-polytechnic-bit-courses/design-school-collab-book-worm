using UnityEngine;

public class TeleportUnderMap : MonoBehaviour
{
    private CharacterController player;
    public MeshRenderer sphereMesh;
    private MeshRenderer teleportCube;
    private float undergroundY = -50f;

    private void Start()
    {
        teleportCube = GetComponent<MeshRenderer>();
        teleportCube.enabled = false;
    }

    private void Update()
    {

        if (sphereMesh.enabled && !teleportCube.enabled)
        {
            teleportCube.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<CharacterController>();

        if (player != null && sphereMesh.enabled)
        {
            player.enabled = false;
            Vector3 pos = player.transform.position;
            player.transform.position = new Vector3(pos.x, undergroundY, pos.z);
            player.enabled = true;
        }
    }
}