using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTeleport : MonoBehaviour
{
 
    public MeshRenderer sphereMesh;
    private MeshRenderer teleportCube;
    [SerializeField] private string sceneName;

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
        if (other.CompareTag("Player") && (teleportCube.enabled))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
