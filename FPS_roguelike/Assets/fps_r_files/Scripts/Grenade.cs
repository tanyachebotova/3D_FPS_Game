using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float initialForce;
    public float damage;
    public float explosionRadius;
    public LayerMask enemyLayer;
    public int grenadesPerBlock;

    private Rigidbody rb;
    private int grenadesRemaining;

    // Для отслеживания приземления
    private bool hasLanded;
    private float lastUpdateTimestamp;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grenadesRemaining = grenadesPerBlock;
        hasLanded = false;
        lastUpdateTimestamp = Time.time;
    }

    public void ThrowGrenade(Vector3 forceDirection)
    {
        if (grenadesRemaining > 0)
        {
            grenadesRemaining--;
            rb.AddForce(forceDirection * initialForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("No more grenades in this block!");
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded && collision.gameObject.CompareTag("Ground")) // Проверка на столкновение с землей
        {
            // Инициализация флага приземления
            hasLanded = true;
            lastUpdateTimestamp = Time.time; // Запоминаем время приземления
            Explode();
            Destroy(gameObject, 0.5f); // Уничтожение гранаты после взрыва 
        }
    }
    // Метод для взрыва
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

        foreach (Collider hitCollider in colliders)
        {
            // Находим компонент Health для противника
            Health enemyHealth = hitCollider.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }


    private void Update()
    {
        // Проверка, прошло ли достаточно времени для приземления
        if (hasLanded)
        {

            float timeSinceLanding = Time.time - lastUpdateTimestamp;
            if (timeSinceLanding > 1f)
            { // или сколько вам нужно времени, чтобы граната "остановилась"
                Explode();
                Destroy(gameObject);
            }
        }
       

    }



}

