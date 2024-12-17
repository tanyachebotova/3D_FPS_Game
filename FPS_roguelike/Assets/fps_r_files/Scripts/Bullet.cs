using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // количество урона, наносимого пулей

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, есть ли у объекта компонент Health
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject); // уничтожаем пулю после попадания
        }
    }
}


