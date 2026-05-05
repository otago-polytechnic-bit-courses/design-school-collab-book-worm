using UnityEditor.Build;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    bool isCorrupted;
    [SerializeField] GameObject Player;
    Vector3 playerPosition;
    Vector3 enemyPosition;
    Vector3 direction;
    float speed = 3.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Move();

    }
    void Move()
    {
        playerPosition = Player.transform.position;
        enemyPosition = transform.position;

        direction = playerPosition - enemyPosition;
        direction.y = 0;
        direction = direction.normalized;

        transform.rotation = Quaternion.LookRotation(direction);

        //algorithm
        transform.position += direction * speed * Time.deltaTime;
    }
}
