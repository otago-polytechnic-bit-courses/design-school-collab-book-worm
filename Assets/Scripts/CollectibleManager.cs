using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int collectedCount = 0;
    [SerializeField] private int requiredToOpenDoor = 5;

    [SerializeField] private GameObject bossDoor;
    

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        bossDoor.SetActive(false);
    }
    public void Collect()
    {
        collectedCount++;

        if (collectedCount >= requiredToOpenDoor)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        bossDoor.SetActive(true);
    }
}