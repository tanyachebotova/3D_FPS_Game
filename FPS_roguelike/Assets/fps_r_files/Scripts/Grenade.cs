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

    // ��� ������������ �����������
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
        if (!hasLanded && collision.gameObject.CompareTag("Ground")) // �������� �� ������������ � ������
        {
            // ������������� ����� �����������
            hasLanded = true;
            lastUpdateTimestamp = Time.time; // ���������� ����� �����������
            Explode();
            Destroy(gameObject, 0.5f); // ����������� ������� ����� ������ 
        }
    }
    // ����� ��� ������
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);

        foreach (Collider hitCollider in colliders)
        {
            // ������� ��������� Health ��� ����������
            Health enemyHealth = hitCollider.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }


    private void Update()
    {
        // ��������, ������ �� ���������� ������� ��� �����������
        if (hasLanded)
        {

            float timeSinceLanding = Time.time - lastUpdateTimestamp;
            if (timeSinceLanding > 1f)
            { // ��� ������� ��� ����� �������, ����� ������� "������������"
                Explode();
                Destroy(gameObject);
            }
        }
       

    }



}

