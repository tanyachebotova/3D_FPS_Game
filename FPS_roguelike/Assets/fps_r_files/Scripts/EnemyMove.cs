using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public NavMeshAgent agent;  // Ссылка на NavMeshAgent
    public Transform player;    // Ссылка на игрока
    public float moveSpeed = 1f;  // Настройка скорости движения врага (публичная переменная)

    void Start()
    {
        agent.speed = moveSpeed;
    }

    void Update()
    {
        if (player != null && agent != null)
        {
            // Устанавливаем позицию игрока как цель для NavMeshAgent
            agent.SetDestination(player.position);
        }
    }
}
