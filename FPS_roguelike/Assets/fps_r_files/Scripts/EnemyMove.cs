using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public NavMeshAgent agent;  // Ссылка на NavMeshAgent
    public Transform player;    // Ссылка на игрока
    public float moveSpeed = 1f;  // Настройка скорости движения врага (публичная переменная)

    void Start()
    {
        // Если агент не назначен через Inspector, ищем компонент на этом объекте
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Если игрок не назначен вручную, ищем его по тегу "Player"
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Игрок с тегом 'Player' не найден на сцене!");
            }
        }

        // Устанавливаем начальную скорость врага через NavMeshAgent
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
