using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public float chaseRange = 10f; // Distance at which chase starts
    public int health = 3;
    public int damage = 1;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            agent.SetDestination(player.transform.position);
        }
        if (health <= 0)
        {
            Debug.Log("Enemy is Dead!");
            Destroy(gameObject);
        }
    }



}